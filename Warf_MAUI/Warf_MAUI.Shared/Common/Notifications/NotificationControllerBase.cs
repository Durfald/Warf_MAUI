using Microsoft.JSInterop;
using System.Reflection;
using System.Runtime.Serialization;
using Warf_MAUI.Shared.Services;

namespace Warf_MAUI.Shared.Common.Notifications
{
  

    public enum NotifyType
    {
        primary,
        success,
        warning,
        danger
    }

    public enum NotifyPosition
    {
        [EnumMember(Value = "top-left")]
        TopLeft,

        [EnumMember(Value = "top-center")]
        TopCenter,

        [EnumMember(Value = "top-right")]
        TopRight,

        [EnumMember(Value = "bottom-left")]
        BottomLeft,

        [EnumMember(Value = "bottom-center")]
        BottomCenter,

        [EnumMember(Value = "bottom-right")]
        BottomRight
    }

    public abstract class NotificationControllerBase(ApplicationSettings applicationSettings) : INotificationController
    {
        private string GetEnumMemberValue(Enum value)
        {
            var type = value.GetType();
            var member = type.GetMember(value.ToString()).FirstOrDefault();
            var attr = member?.GetCustomAttribute<EnumMemberAttribute>();
            return attr?.Value ?? value.ToString();
        }
        public void NofityAll(string message, IJSRuntime runtime,NotifyType type = NotifyType.primary,NotifyPosition pos = NotifyPosition.TopRight, int timeout = 500)
        {
            NotifyDiscord(message);
            NotifyTelegram(message);
            NotifyApplication(runtime, message, type, pos, timeout);
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

        public void NotifyApplication(IJSRuntime runtime,string message, NotifyType type = NotifyType.primary,NotifyPosition pos = NotifyPosition.TopRight, int timeout = 500)
        {
            if (!applicationSettings.Notifications.EnableSendNotificationsThroughApplication)
                return;
            runtime.InvokeVoidAsync("UIkit.notification",new {message, status = type.ToString(), pos = GetEnumMemberValue(pos), timeout});
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

        #endregion Overridable methods
    }
}
