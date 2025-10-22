using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Microsoft.Maui.LifecycleEvents;
using Warf_MAUI.Shared.Common.Notifications;
using Warf_MAUI.Shared.Services;

namespace Warf_MAUI.Platforms.Windows
{
    internal static partial class ApplicationBuilder
    {
        internal static void AddBuildForWindows(this MauiAppBuilder appBuilder)
        {
            appBuilder.ConfigureLifecycleEvents(config =>
            {
                config.AddWindows(lifecycleBuilder =>
                    WindowsLifeCycleEvents.RegisterEvents(lifecycleBuilder, appBuilder));
            });

            //appBuilder.Services.AddSingleton<INotificationController>(builder =>
            //{
            //    var settings = builder.GetService<ApplicationSettings>();
            //    if (settings != null)
            //        return new WindowsNotificationController(
            //            settings,
            //            // Action to show notifications using Windows Toast Notifications (not available in Shared project)
            //            content =>
            //            {
            //                switch (content.ContentType)
            //                {
            //                    case WindowsNotificationContext.ContificationContentType.SimpleText:
            //                        WindowsNotifications.GenericText(content.Message);
            //                        break;
            //                    // Потом можно добавить условия и другие нотификации   
            //                    // ...
            //                    default:
            //                        throw new NotImplementedException(
            //                            "WindowsNotificationContext.ContificationContentType is not " +
            //                            "implemented for this ContentType of notification: " +
            //                            Enum.GetName(content.ContentType));

            //                }
            //            });

            //    throw new InvalidOperationException(
            //            "ApplicationSettings is not registered in the service collection. " +
            //            "You should register it before INotificationController.");
            //});
        }
    }
}
