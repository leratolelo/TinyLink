namespace TinyLink
{
    public class TinyLink
    {
        private readonly string _baseUrl = "https://tinyurl.com/";

        public Guid Id { get; set; }
        public string OrginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public string HashOfUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired => ExpirationDate < DateTime.Now;
    }
}
