
using TinyLink.API.Infrastructure;

namespace TinyLink.API.Services
{
    public class VisitService : IVisitService
    {
        private readonly IGenericRepository<Models.Visit> _genericRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VisitService(IGenericRepository<Models.Visit> genericRepository, IHttpContextAccessor httpContextAccessor)
        {
            _genericRepository = genericRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Models.Visit> GetVisitsByLinkId(Guid tinyLinkId)
        {
            return _genericRepository.GetByCondition(v => v.LinkId == tinyLinkId);

        }

        private void RecordVisit(Models.Visit visit)
        {
            var existingVisit = _genericRepository.GetByCondition(v => v.LinkId == visit.LinkId).FirstOrDefault();
            if (existingVisit != null)
            {
                existingVisit.Count++;
                _genericRepository.Update(existingVisit);
            }
            else
            {
                visit.Count++;
                _genericRepository.Add(visit);
            }
            _genericRepository.Save();


        }

        public void RecordVisit(Models.TinyLink link)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = httpContext.Request.Headers.UserAgent.ToString() ?? string.Empty;
            var referrer = httpContext.Request.Headers.Referer.ToString() ?? string.Empty;
            var cookies = string.Join(",", httpContext.Request.Cookies.Select(x => $"Key={x.Key}\tValue={x.Value}"));
            var headers = string.Join(",", httpContext.Request.Headers);
            var extras = httpContext.Request.Headers.UserAgent.ToString() ?? string.Empty;

            var visit = new Models.Visit
            {
                Device = userAgent,
                Referrer = referrer,
                Cookies = cookies,
                Headers = headers,
                IpAddress = ipAddress,
                Id = Guid.NewGuid(),
                Extras = extras,
                LinkId = link.Id,
                UserId = link.UserId
            };

            RecordVisit(visit);
        }
    }
}
