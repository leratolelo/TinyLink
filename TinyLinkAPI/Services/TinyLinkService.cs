using Microsoft.AspNetCore.Identity;
using TinyLink.API.Commands;
using TinyLink.API.Infrastructure;
using TinyLink.API.Models;
using TinyLink.API.Queries;


namespace TinyLink.API.Services
{
    public class TinyLinkService : ITinyLinkService
    {
        
        private readonly IGenericRepository<Models.TinyLink> _genericRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public TinyLinkService(IGenericRepository<Models.TinyLink> genericRepository , IHttpContextAccessor httpContextAccessor)
        {
            _genericRepository = genericRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public Models.TinyLink CreateTinyLink(CreateTinyLinkCommand command)
        {
            var baseUrl =  $"{_httpContextAccessor.HttpContext.Request.Scheme}//{_httpContextAccessor.HttpContext.Request.Host.Value}/";
            LinkGenerator.SetBaseURL(baseUrl);
            HashHelper hashHelper = new HashHelper();
            string hash = hashHelper.GenerateHashUrl(command.LongLink);
            var shortLink = LinkGenerator.GenerateShortLink(baseUrl , hash);

            var newTinyLink = new Models.TinyLink
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                LongLink = command.LongLink,
                ShortLink = shortLink,
                Hash = hash,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddMonths(1),
                Deativated = false
            };

            _genericRepository.Add(newTinyLink);
            _genericRepository.Save();
            return newTinyLink;
        }

        public string GetOriginalLink(ConnectToTinyLinkQuery query)
        {
            var hash = query.TinyLink.Split('/').Last();
            var tinyLink = _genericRepository.GetByCondition(x => x.Hash == hash).FirstOrDefault();
            return tinyLink?.LongLink ?? string.Empty;
        }

        void ITinyLinkService.DeleteTinyLink(Guid id)
        {
            var link = _genericRepository.GetById(id);
            if(link != null )
            {
                link.Deativated = true;
                _genericRepository.Update(link);
                _genericRepository.Save();
            }
        }

        void ITinyLinkService.UpdateTinyLink(TinyLinkDto dto)
        {
            var link = _genericRepository.GetById(dto.Id);
            if (link != null)
            {
                link.LongLink = dto.LongLink;
                link.ModifiedDate = DateTime.Now;
                link.Deativated = dto.Deactivated;
                _genericRepository.Update(link);
                _genericRepository.Save();
            }
        }

        public TinyLinkDto MapToDto(Models.TinyLink link)
        {
            return new TinyLinkDto
            {
                Id = link.Id,
                Deactivated = link.Deativated,
                CreatedDateTime = link.CreatedDate,
                ShortLink = link.ShortLink,
                LongLink = link.LongLink,
                UserId = link.UserId
            };
        }

        public IEnumerable<TinyLinkDto> GetAllTinyLinks()
        {
           
            return _genericRepository.GetAll().Where(x => !x.Deativated).Select(MapToDto);
        }

        public IEnumerable<TinyLinkDto> GetTinyLinksByUserId(Guid userId)
        {

            return _genericRepository.GetAll().Where(x => !x.Deativated && x.UserId == userId).Select(MapToDto);
        }

    }



}
