using TodoApi.Models;

namespace AdminControl.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? password { get; set; }
        public bool isblock { get; set; } = false;
        public string? confirm { get; set; }
        public bool Isconfirm { get; set; } = false;
        public string? token { get; set; }=Guid.NewGuid().ToString();
        public bool isDelete { set;get; } = false; 
        public virtual ICollection<FavoritesModel>? Favorites { get; set; } // ربط بقائمة المفضلات
        public virtual ICollection<AdModel>? Ad { get; set; }
        public virtual ICollection<NotificationCustomer>? NotificationCustomer { get; set; }
        public virtual ICollection<AdView>? AdView { get; set; }
        public virtual ICollection<NotificationView>? NotificationView { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }

    }



}
