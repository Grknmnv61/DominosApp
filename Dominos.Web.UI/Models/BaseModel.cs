using Dominos.Common.Enums.Notification;

namespace Dominos.Web.UI.Models
{
    public class BaseModel
    {
        public bool Result { get; set; }
        public string ValidationMessages { get; set; }
        public int TotalItemCount { get; set; }
        public NotificationTypes NotificationTypes { get; set; }
    }
}