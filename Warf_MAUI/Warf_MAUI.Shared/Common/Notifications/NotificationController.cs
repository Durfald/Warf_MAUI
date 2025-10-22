using Microsoft.JSInterop;
using Warf_MAUI.Shared.Services;

namespace Warf_MAUI.Shared.Common.Notifications
{
    public class NotificationController : NotificationControllerBase
    {
        public NotificationController(ApplicationSettings applicationSettings)
            : base(applicationSettings)
        {
        }
    }
}
