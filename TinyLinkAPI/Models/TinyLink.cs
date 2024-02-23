namespace TinyLink.API.Models
{
    public class TinyLink
    {
        public Guid Id { get; set; }

       public Guid UserId { get; set; }
        public string LongLink { get; set; }
        public string Hash { get; set; }
        public string ShortLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Deativated { get; set; }
    }

}
