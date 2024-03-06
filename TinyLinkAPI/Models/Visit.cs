namespace TinyLink.API.Models
{
    public class Visit : Entity
    {
        public int Count { get; set; }
        public DateTime Timestamp { get; set; }  = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string? Device { get; set; }
        public string? Extras { get; set; }
        public string? Cookies { get; set; }
        public string? Headers { get; set; }
        public string? Referrer { get; set; }
        public Guid LinkId { get; set; }
        public Guid? UserId { get; set; } 
        public TinyLink Link { get; set; }
    }
}
