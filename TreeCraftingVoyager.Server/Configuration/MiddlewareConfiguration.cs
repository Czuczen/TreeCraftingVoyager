using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace TreeCraftingVoyager.Server.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigins");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("/index.html");
            });
        }
    }
}
