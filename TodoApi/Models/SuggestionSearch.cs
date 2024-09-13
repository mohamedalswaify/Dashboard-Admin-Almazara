namespace AdminControl.Models
{
    public class SuggestionSearch
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? CreatedDate { get; set; }=DateTime.Now;



    }
}
