using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TreeCraftingVoyager.Server.Configuration;

public static class JwtConfiguration
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogDebug("OnMessageReceived triggered");

                        foreach (var header in context.Request.Headers)
                        {
                            logger.LogDebug("Header: {0} - {1}", header.Key, header.Value);
                        }

                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                        logger.LogDebug("Authorization Header: {0}", authHeader);

                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            var token = authHeader.Substring("Bearer ".Length).Trim();
                            logger.LogDebug("Extracted Token: {0}", token);
                            logger.LogDebug("Token from context: {0}", context.Token);

                            if (token.Split('.').Length == 3)
                            {
                                logger.LogDebug("Token is correct: {0}", token);
                            }
                            else
                            {
                                logger.LogWarning("Token format is incorrect: {0}", token);
                            }
                        }
                        else
                        {
                            logger.LogWarning("Authorization header is missing or does not start with 'Bearer '");
                        }

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogDebug("Token validated for {0}", context.Principal?.Identity?.Name);
                        logger.LogDebug("Validated Token: {0}", context.SecurityToken);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        var token = authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : "No token found";

                        logger.LogDebug("Authentication failed: {0}", context.Exception.Message);
                        logger.LogDebug("Failed Token: {0}", token);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        var token = authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : "No token found";

                        logger.LogDebug("OnChallenge: {0}", context.ErrorDescription);
                        logger.LogDebug("Token on Challenge: {0}", token);
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        var token = authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : "No token found";

                        logger.LogDebug("OnForbidden: {0}", context.HttpContext.Request.Path);
                        logger.LogDebug("Token on Forbidden: {0}", token);
                        return Task.CompletedTask;
                    }
                };

                if (builder.Environment.IsDevelopment())
                {
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 }
                };
            });

        return builder;
    }
}
