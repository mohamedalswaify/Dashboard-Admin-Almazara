using AdminControl.Models;

namespace TodoApi.Models
{
    public class NotificationView
    {
        public int Id { get; set; }
        public int NotificationModelId { get; set; }
        public int CsutomerId { get; set; }
        public DateTime ViewedAt { get; set; }
        public virtual CustomerModel CustomerModel { get; set; }
        public virtual NotificationModel Notification { get; set; }
    }
}
