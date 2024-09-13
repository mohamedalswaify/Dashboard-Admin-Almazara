namespace TodoApi.Models
{
    public class CategoryViewsResult
    {
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ViewCount { get; set; }
        public int PendingAds { get; set; }
        public int ApprovedAds { get; set; }
        public int RejectedAds { get; set; }
    }
}
