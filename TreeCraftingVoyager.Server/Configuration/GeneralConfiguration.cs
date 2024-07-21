using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TreeCraftingVoyager.Server.Constants;
using TreeCraftingVoyager.Server.Data;
using TreeCraftingVoyager.Server.Models.Management;

namespace TreeCraftingVoyager.Server.Configuration;

public static class GeneralConfiguration
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        return builder;
    }

    public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<Account, Role>(options =>
        {
            //options.SignIn.RequireConfirmedAccount = true; // soon
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return builder;
    }

    public static WebApplicationBuilder AddCorsPolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                corsBuilder =>
                {
                    corsBuilder.WithOrigins(builder.Environment.IsDevelopment() ? ClientApps.OriginsListDev : ClientApps.OriginsListProd)
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();

                    // needed?
                    //.SetIsOriginAllowed(origin =>
                    //{
                    //     if (string.IsNullOrWhiteSpace(origin)) return false;

                    //     var allowedOrigins = builder.Environment.IsDevelopment() ? ClientApps.OriginsListDev : ClientApps.OriginsListProd;

                    //     return allowedOrigins.Contains(origin);
                    //});
                });
        });

        return builder;
    }

    public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
           // .AddDataAnnotationsLocalization(); // needed?

        return builder;
    }

    public static WebApplicationBuilder AddHttpsRedirection(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 7265;
            });
        }
        else
        {
            builder.Services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;

                // in far future
                options.HttpsPort = 443; // Make sure the port is correct for your hosting
            });
        }
        
        return builder;
    }

    public static WebApplicationBuilder AddAuthorizationPolicies(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
            options.AddPolicy("VendorPolicy", policy => policy.RequireRole("Vendor"));

            // soon
            //options.AddPolicy("CanEditProducts", policy =>
            //    policy.RequireClaim("permission", "edit_products"));
            //options.AddPolicy("CanViewOrders", policy =>
            //    policy.RequireClaim("permission", "view_orders"));
            //options.AddPolicy("CanManageInventory", policy =>
            //    policy.RequireClaim("permission", "manage_inventory"));
        });

        return builder;
    }

    public static WebApplicationBuilder AddMemoryCache(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();

        return builder;
    }
    
}
