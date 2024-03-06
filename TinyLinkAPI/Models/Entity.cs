namespace TinyLink.API.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDateTime { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set;}


    }
}
