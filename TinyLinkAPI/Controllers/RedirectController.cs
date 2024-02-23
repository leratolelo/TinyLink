using Microsoft.AspNetCore.Mvc;
using TinyLink.API.Queries;
using TinyLink.API.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TinyLink.API.Controllers
{

    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly ITinyLinkService _tinyLinkService;
        public RedirectController(ITinyLinkService tinyLinkService)
        {
            _tinyLinkService = tinyLinkService;
        }

        [HttpGet]
        [Route("/{code}")]
        public IActionResult Get(string code)
        {
            var query = new ConnectToTinyLinkQuery
            {
                TinyLink = $"/{code}"
            };
            var link = _tinyLinkService.GetOriginalLink(query);
            return Redirect(link);
        }

      
    }
}
