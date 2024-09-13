namespace AdminControl.Models
{
    public class SuggestionModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string Text { get; set; }

        public bool isRead { get; set; }=false;
    }
}
