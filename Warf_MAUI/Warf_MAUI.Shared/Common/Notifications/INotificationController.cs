namespace Warf_MAUI.Shared.Common.Notifications
{
    public interface INotificationController
    {
        public void NofityAll(string message);
        public void NotifyDiscord(string message);
        public void NotifyTelegram(string message);
        public void NotifyWindows(string message);
        public void NotifyApplication(string message);
    }
}
