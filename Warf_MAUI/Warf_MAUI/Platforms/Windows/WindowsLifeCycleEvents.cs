using Microsoft.Maui.LifecycleEvents;
using Warf_MAUI.Shared.Services;

namespace Warf_MAUI.Platforms.Windows
{
    public static class WindowsLifeCycleEvents
    {
        private static MauiAppBuilder _appBuilderRef = null!;

        public static void RegisterEvents(IWindowsLifecycleBuilder builder, MauiAppBuilder appBuilder)
        {
            _appBuilderRef = appBuilder;
            builder.OnClosed(OnClosedDelegate);
        }

        public static void OnClosedDelegate(Microsoft.UI.Xaml.Window window, Microsoft.UI.Xaml.WindowEventArgs args)
        {
            if (_appBuilderRef.Services.First(x => x.ServiceType == typeof(ApplicationSettings))
                .ImplementationInstance is ApplicationSettings settings) 
                ApplicationSettings.SaveSettings(settings);
        }

        // ...
    }
}
