using Microsoft.AspNetCore.Mvc;

namespace TinyLink.API.Queries
{
    public class UserQuery
    {
        [FromQuery]
        public Guid Id { get; set; }
    }

}
