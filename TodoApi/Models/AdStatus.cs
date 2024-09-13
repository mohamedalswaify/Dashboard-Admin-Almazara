namespace AdminControl.Models
{
    public class AdStatus
    {
        public int Id { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<AdModel>? AdModels { get;}
    }
}
