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

                        foreach (var header in context.Request.Headers)
                        {
                            logger.LogInformation("Header: {0} = {1}", header.Key, header.Value);
                        }

                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                        logger.LogInformation("Authorization Header: {0}", authHeader);

                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            var token = authHeader.Substring("Bearer ".Length).Trim();
                            logger.LogInformation("Extracted Token: {0}", token);

                            if (token.Split('.').Length == 3)
                            {
                                context.Token = token;
                                logger.LogInformation("Token successfully set in context: {0}", context.Token);
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
                        logger.LogInformation("Token validated for {0}", context.Principal.Identity.Name);
                        logger.LogInformation("Validated Token: {0}", context.SecurityToken.SecurityKey);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        var token = authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : "No token found";

                        logger.LogError("Authentication failed: {0}", context.Exception.Message);
                        logger.LogError("Failed Token: {0}", token);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        var token = authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : "No token found";

                        logger.LogInformation("OnChallenge: {0}", context.ErrorDescription);
                        logger.LogInformation("Token on Challenge: {0}", token);
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        var token = authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : "No token found";

                        logger.LogInformation("OnForbidden: {0}", context.HttpContext.Request.Path);
                        logger.LogInformation("Token on Forbidden: {0}", token);
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        return builder;
    }
}
