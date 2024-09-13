namespace AdminControl.Models
{
    public class WhatsAppClick
    {

        public int Id { get; set; }
        public int AdId { get; set; }
        public int UserId { get; set; }
        public DateTime ClickedAt { get; set; }

        public virtual AdModel Ad { get; set; }
    }
}
