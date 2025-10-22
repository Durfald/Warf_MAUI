using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Warf_MAUI.Shared.Common.Notifications
{
    public interface INotificationController
    {
        public void NofityAll(string message, IJSRuntime runtime);
        public void NotifyDiscord(string message);
        public void NotifyTelegram(string message);
        public void NotifyWindows(string message);
        public void NotifyApplication(string message, IJSRuntime runtime);
    }
}