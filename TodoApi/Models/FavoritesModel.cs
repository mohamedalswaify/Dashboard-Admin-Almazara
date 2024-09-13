namespace AdminControl.Models
{
    public class FavoritesModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual CustomerModel? Customer { get; set; } // ربط بالعميل
        public int AdId { get; set; }
        public virtual AdModel? Ad { get; set; } // ربط بالإعلان
    }
}
