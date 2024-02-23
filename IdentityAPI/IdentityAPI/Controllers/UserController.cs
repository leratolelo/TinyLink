using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdentityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("UserByEmail")]
        public async Task<IActionResult> UserByEmail(UserQuery userQuery)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userQuery.Email);
            if (user == null)
            {
                return NotFound();
            }
            var appUserDto = new AppUserDto
            {
                Email = user.Email,
                Id = user.Id,
                Username = user.UserName.Split("@").First()

            };

            return Ok(appUserDto);
        }
    }
}
