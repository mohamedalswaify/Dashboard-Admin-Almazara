using TodoApi.Models;

namespace AdminControl.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string? Token { get; set; }=Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public string? MessageBody { get; set; }
        public string? Image { get; set; } = null;
        public int? AdModelId { get; set; } = null;
        public int? NumberOfSend { get; set; } = 0;
        public virtual AdModel? adModel { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual ICollection<NotificationCustomer> NotificationCustomer { get; set; }
        public virtual ICollection<NotificationView> NotificationView { get; set; }

    }
}
