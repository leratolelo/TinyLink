using TinyLink.API.Models.DTOs;

namespace TinyLink.API.Models
{
    public class TinyLink : Entity
    {
       public Guid UserId { get; set; }
        public string LongLink { get; set; }
        public string Hash { get; set; }
        public string ShortLink { get; set; }

        public TinyLinkDto MapToDto()
        {
            return new TinyLinkDto
            {
                Id = Id,
                Deactivated = Deleted,
                CreatedDateTime = CreatedDateTime,
                ShortLink = ShortLink,
                LongLink = LongLink,
                UserId = UserId,
                Hash = Hash
            };
        }
    }

}
