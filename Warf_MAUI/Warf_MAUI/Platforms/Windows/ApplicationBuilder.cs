using Microsoft.Maui.LifecycleEvents;

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
        }
    }
}
