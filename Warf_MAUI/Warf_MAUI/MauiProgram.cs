using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Warf_MAUI.Services;
using Warf_MAUI.Shared.Services;

#if WINDOWS
using Warf_MAUI.Platforms.Windows;
#endif

namespace Warf_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the Warf_MAUI.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton(new ApplicationSettings(overrides =>
            {
                overrides.DoBeFurry = true;
            }));

#if WINDOWS
            builder.AddBuildForWindows();
#endif

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            return app;
        }
    }
}
