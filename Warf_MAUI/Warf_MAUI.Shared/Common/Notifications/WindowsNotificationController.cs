using Warf_MAUI.Shared.Services;
namespace Warf_MAUI.Shared.Common.Notifications
{
    /// <summary>
    /// Будет рассылать уведомления в приложении на Windows через <see cref="INotificationController" />, 
    /// который реализует <see cref="NotificationControllerBase"/>.
    /// </summary>
    /// <param name="applicationSettings"></param>
    public class WindowsNotificationController(
        ApplicationSettings applicationSettings,
        Action<WindowsNotificationContext> WindowsSpecificNotificationAction
        ) : NotificationControllerBase(applicationSettings)
    {
        protected override Task NotifyApplicationAsync(string message)
        {
            return base.NotifyApplicationAsync(message);
        }

        protected override Task NotifyDiscordAsync(string message)
        {
            return base.NotifyDiscordAsync(message);
        }

        protected override Task NotifyTelegramAsync(string message)
        {
            return base.NotifyTelegramAsync(message);
        }

        protected override Task NotifyWindowsAsync(string message)
        {
            WindowsSpecificNotificationAction.Invoke(
                new WindowsNotificationContext
                {
                    Message = message,
                    ContentType = WindowsNotificationContext.ContificationContentType.SimpleText
                });

            return Task.CompletedTask;
        }
    }
}
