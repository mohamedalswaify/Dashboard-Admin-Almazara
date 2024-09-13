using AdminControl.Models;
using FirebaseAdmin.Messaging;

namespace TodoApi.Models
{
    public class NotificationCustomer
    {
        public int Id { get; set; }
        public int NotificationModelId { get; set; }
        public virtual NotificationModel Notification { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;  
        public int CustomerModelId { get; set; }
        public virtual CustomerModel customer { get; set; }
    }
}
