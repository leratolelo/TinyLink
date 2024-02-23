using Microsoft.AspNetCore.Mvc;

namespace TinyLink.API.Models
{
    public class UserQuery
    {
        [FromQuery]
        public Guid Id { get; set; }
    }

}
