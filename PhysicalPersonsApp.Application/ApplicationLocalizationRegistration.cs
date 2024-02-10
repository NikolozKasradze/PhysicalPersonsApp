using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace PhysicalPersonsApp.Application;

public static class ApplicationLocalizationRegistration
{
    public static IApplicationBuilder RegisterLocalization(this IApplicationBuilder app)
    {
        app.UseRequestLocalization(new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture("ka"),
            SupportedCultures = new[] { new CultureInfo("ka"), new CultureInfo("en") },
            SupportedUICultures = new[] { new CultureInfo("ka"), new CultureInfo("en") }
        });

        return app;
    }
}
