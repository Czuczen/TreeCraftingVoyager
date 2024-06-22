using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                    corsBuilder.WithOrigins(ClientApps.OriginsListProd.Concat(ClientApps.OriginsListDev).ToArray())
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                });
        });

        return builder;
    }

    public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddDataAnnotationsLocalization(); // needed?

        return builder;
    }

    public static void UseCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors("AllowSpecificOrigins");
    }

    public static void UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
