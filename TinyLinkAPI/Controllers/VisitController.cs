using Microsoft.AspNetCore.Mvc;
using TinyLink.API.Services;

namespace TinyLink.API.Controllers
{
    public class VisitController : Controller
    {
        private readonly IVisitService _genericService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VisitController(IVisitService genericService, IHttpContextAccessor httpContextAccessor)
        {
            _genericService = genericService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPost]
        [Route("GetVisits")]
        public Task<IActionResult> GetVisits(Guid tinyLinkId)
        {
            throw new NotImplementedException();
        }



    }
}
