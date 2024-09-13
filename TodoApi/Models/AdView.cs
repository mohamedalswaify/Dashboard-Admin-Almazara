using System.ComponentModel.DataAnnotations.Schema;

namespace AdminControl.Models
{
    public class AdView
    {
        public int Id { get; set; }
        public int AdId { get; set; }
        
        [ForeignKey(nameof(CustomerModel))]
        public int UserId { get; set; }
        public DateTime ViewedAt { get; set; }
        public virtual CustomerModel CustomerModel { get; set; }
        public virtual AdModel Ad { get; set; }
    }
}
