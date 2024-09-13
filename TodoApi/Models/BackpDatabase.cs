namespace TodoApi.Models
{
    public class BackpDatabase
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public virtual Employee Employee { get; set; }
        public int EmployeeId { get; set; }

    }
}
