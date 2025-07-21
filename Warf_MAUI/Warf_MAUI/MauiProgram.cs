using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Maui.LifecycleEvents;
using Warf_MAUI.Services;
using Warf_MAUI.Shared.Common.WebAPI.Interfaces;
using Warf_MAUI.Shared.Common.WebAPI.Storage.FileStorage;
using Warf_MAUI.Shared.Common.WebAPI.Storage.MemoryStorage;
using Warf_MAUI.Shared.Common.WebAPI.WebClient;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyApiClient;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyApiClient.Service;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Services;
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

            builder.Services.AddSingleton<IDataStorage,FileDataStorage>();
            builder.Services.AddSingleton<MemoryCacheService>();

            builder.Services.AddSingleton<IHttpClientMy>(provider =>
            {
                return new RestApiClient("http://37.46.19.255:2530/api/", 0);
            });
            builder.Services.AddSingleton<IHttpClientV1>(provider =>
            {
                return new RestApiClient("https://api.warframe.market/v1/");
            }); 
            builder.Services.AddSingleton<IHttpClientV2>(provider =>
            {
                return new RestApiClient("https://api.warframe.market/v2/");
            });

            builder.Services.AddSingleton<IInfoService, InfoService>();
            builder.Services.AddSingleton<MyApiClient>();

            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.AddSingleton<WarframeMarketApiClient>();


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
