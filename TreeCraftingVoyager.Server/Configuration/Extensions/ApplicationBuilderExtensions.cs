using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TreeCraftingVoyager.Server.Configuration.Extensions
{
    public static class ApplicationBuilderExtensions
    {
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
}
