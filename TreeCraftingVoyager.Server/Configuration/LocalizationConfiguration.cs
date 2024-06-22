using Microsoft.Extensions.Options;
using System.Globalization;

namespace TreeCraftingVoyager.Server.Configuration;

public static class LocalizationConfiguration
{
    public static WebApplicationBuilder AddLocalizationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                //new CultureInfo("pl-PL")
            };

            options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        return builder;
    }

    public static void UseLocalizationConfiguration(this IApplicationBuilder app)
    {
        var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(locOptions.Value);
    }
}
