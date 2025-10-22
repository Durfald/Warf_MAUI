using Microsoft.JSInterop;
using Warf_MAUI.Shared.Services;

namespace Warf_MAUI.Shared.Common.Notifications
{
    public abstract class NotificationControllerBase(ApplicationSettings applicationSettings) : INotificationController
    {
        public void NofityAll(string message, IJSRuntime runtime)
        {
            NotifyDiscord(message);
            NotifyTelegram(message);
            NotifyWindows(message);
            NotifyApplication(message, runtime);
        }

        public void NotifyDiscord(string message)
        {
            if (!applicationSettings.Notifications.EnableSendNotificationsThroughDiscord)
                return;

            Task.Run(() => NotifyDiscordAsync(message));
        }

        public void NotifyTelegram(string message)
        {
            if (!applicationSettings.Notifications.EnableSendNotificationsThroughTelegram)
                return;

            Task.Run(() => NotifyTelegramAsync(message));
        }

        public void NotifyWindows(string message)
        {
            if (!applicationSettings.Notifications.EnableSendNotificationsThroughWindows)
                return;

            Task.Run(() => NotifyWindowsAsync(message));
        }

        public void NotifyApplication(string message, IJSRuntime runtime)
        {
            if (!applicationSettings.Notifications.EnableSendNotificationsThroughApplication)
                return;

            runtime.InvokeVoidAsync("UIkit.notification", message);
        }


        #region Overridable methods

        protected virtual async Task NotifyDiscordAsync(string message)
        {
            await Task.Delay(1);
        }

        protected virtual async Task NotifyTelegramAsync(string message)
        {
            await Task.Delay(1);
        }

        protected virtual async Task NotifyWindowsAsync(string message)
        {
            await Task.Delay(1);
        }

        #endregion Overridable methods
    }
}
