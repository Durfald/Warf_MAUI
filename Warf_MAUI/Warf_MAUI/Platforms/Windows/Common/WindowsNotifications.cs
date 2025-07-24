using Microsoft.Toolkit.Uwp.Notifications;

namespace Warf_MAUI.Platforms.Windows
{
    public static class WindowsNotifications
    {
        public static void GenericText(string message)
        {
            new ToastContentBuilder()
                .AddText(message)
                .Show();
        }
    }
}
