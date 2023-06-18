namespace TestProject.Domain.Commons
{
    public class Auditable
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedTime { get; set; }
    }
}
