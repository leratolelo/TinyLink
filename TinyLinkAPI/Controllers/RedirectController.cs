using Microsoft.AspNetCore.Mvc;
using TinyLink.API.Models;
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
        private readonly IVisitService _visitService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RedirectController(ITinyLinkService tinyLinkService, IVisitService visitService, IHttpContextAccessor httpContextAccessor)
        {
            _tinyLinkService = tinyLinkService;
            _visitService = visitService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        [Route("/{code}")]
        public IActionResult Get(string code)
        {
            var query = new ConnectToTinyLinkQuery
            {
                TinyLink = $"/{code}"
            };
            var link = _tinyLinkService.GetTinyLink(query);
            _visitService.RecordVisit(link);
            return Redirect(link.LongLink);
        }

        


    }
}

