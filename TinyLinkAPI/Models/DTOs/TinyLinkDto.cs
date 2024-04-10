using System.ComponentModel.DataAnnotations;

namespace TinyLink.API.Models.DTOs
{
    public class TinyLinkDto
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Deactivated { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ShortLink { get; set; }
        public string LongLink { get; set; }
        public string Hash { get; set; }
    }
}
