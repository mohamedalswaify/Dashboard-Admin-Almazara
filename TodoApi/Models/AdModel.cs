using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApi.Models;

namespace AdminControl.Models
{
    public class AdModel
    {
        [Key]
        public int Id { get; set; }
        public string? Token { get; set; }=Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; } 
        public virtual Category? Category { get; set; }
        public int AdTypeId { get; set; } 
        public virtual AdType? AdType { get; set; }
        public decimal Price { get; set; } = 0;
        public int AreaId { get; set; } = 1;
        public virtual Area? Area { get; set; }
        public string? MobileNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string? NameVideo { get; set; }
        public int? numberShow { get; set; } = 0;
        public int? numberOfWhatsApp{ get; set; } = 0;
        public int? numberOfCall { get; set; } = 0;
        public virtual List<AttachmentModel>? Attachments { get; set; }
        [NotMapped]
        public List<IFormFile>? Images { get; set; }
        [NotMapped]
        public IFormFile? Video { get; set; }
        public bool IsHide { get; set; }=false;
        public double? Space { get; set; } = 0;
        public int? NumberOfRooms { get; set; } = 0;
        public int? NumberOfBathrooms { get; set; } = 0;
        public int CustomerId { get; set; }
        public virtual CustomerModel? Customer { get; set; } // ربط بالعميل
        public int? AdStatusId { get; set; } // حالة الإعلان
        public virtual AdStatus? AdStatus { get; set; }
        public DateTime? AddedDate { get; set; } = DateTime.Now; // تاريخ الإضافة (تلقائي)
        public DateTime? PublishDate { get; set; } // تاريخ النشر (يمكن أن يكون فارغًا)
        public DateTime? ExpiryDate => PublishDate?.AddDays(30) ?? DateTime.Now.AddDays(30); // تاريخ الانتهاء (تلقائي)

        public virtual ICollection<FavoritesModel>? Favorites { get; set; } // ربط بقائمة المفضلات


        public virtual ICollection<AdView> AdViews { get; set; } = new List<AdView>();
        public virtual ICollection<WhatsAppClick> WhatsAppClicks { get; set; } = new List<WhatsAppClick>();
        public virtual ICollection<CallClick> CallClicks { get; set; } = new List<CallClick>();
        public virtual ICollection<NotificationModel> NotificationModel { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }

    }




}
