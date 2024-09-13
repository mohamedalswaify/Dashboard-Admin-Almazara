using AdminControl.Models;

namespace TodoApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int CustomerModelId { get; set; }
        public int AdModelId { get; set; }
        public string Reason { get; set; }
        public string? Comment { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;

        // علاقات مع المستخدم والإعلان
        public virtual CustomerModel? customerModel { get; set; }
        public virtual AdModel? AdModel { get; set; }
    }
}
