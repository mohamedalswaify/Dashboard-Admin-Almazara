using System.Text.Json.Serialization;

namespace AdminControl.Models
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public string? nameimage { get; set; }
        public int AdId { get; set; }
        [JsonIgnore]
        public virtual AdModel? Ad { get; set; } // ربط بالإعلان
    }
}
