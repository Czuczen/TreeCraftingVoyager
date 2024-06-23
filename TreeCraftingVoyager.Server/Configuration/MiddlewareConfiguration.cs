using System.IdentityModel.Tokens.Jwt;

namespace TreeCraftingVoyager.Server.Configuration;

public static class MiddlewareConfiguration
{
    public static void ConfigureMiddlewares(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("AllowSpecificOrigins");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("/index.html");
        });

        app.UseLocalizationConfiguration();
    }
}
