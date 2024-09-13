namespace TodoApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsHide { get; set; }=false;
        public bool IsDelete { get; set; } = false;
        public virtual ICollection<BackpDatabase> BackpDatabases { get; set; }

    }
}
