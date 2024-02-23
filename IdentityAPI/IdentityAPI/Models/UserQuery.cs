using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Models
{
    public class UserQuery
    {
        [FromQuery]
        public string Email { get; set; }
    }
}
