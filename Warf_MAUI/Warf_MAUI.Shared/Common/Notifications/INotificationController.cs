using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Warf_MAUI.Shared.Common.Notifications
{
    public interface INotificationController
    {
        public void NofityAll(string message, IJSRuntime runtime, NotifyType type, NotifyPosition pos = NotifyPosition.TopRight, int timeout = 500);
        public void NotifyDiscord(string message);
        public void NotifyTelegram(string message);
        public void NotifyApplication(IJSRuntime runtime, string message, NotifyType type = NotifyType.primary, NotifyPosition pos = NotifyPosition.TopRight, int timeout = 500);
    }
}