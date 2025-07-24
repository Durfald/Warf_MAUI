namespace Warf_MAUI.Shared.Common.Notifications
{
    public class WindowsNotificationContext
    {
        public enum ContificationContentType
        {
            SimpleText,
        }

        public string Message { get; init;}

        public ContificationContentType ContentType { get; init; } 
            = ContificationContentType.SimpleText;
    }
}
