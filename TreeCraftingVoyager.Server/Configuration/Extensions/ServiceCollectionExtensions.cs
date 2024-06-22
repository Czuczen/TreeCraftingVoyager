using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TreeCraftingVoyager.Server.Constants;
using TreeCraftingVoyager.Server.Data;
using TreeCraftingVoyager.Server.Models.Management;

namespace TreeCraftingVoyager.Server.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<Account, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
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
        }
    }
}
