using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using TinyLink.API.Commands;
using TinyLink.API.Infrastructure;
using TinyLink.API.Models.DTOs;
using TinyLink.API.Queries;


namespace TinyLink.API.Services
{
    public class TinyLinkService : ITinyLinkService
    {

        private readonly IGenericRepository<Models.TinyLink> _genericRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public TinyLinkService(IGenericRepository<Models.TinyLink> genericRepository, IHttpContextAccessor httpContextAccessor)
        {
            _genericRepository = genericRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public Models.TinyLink CreateTinyLink(CreateTinyLinkCommand command)
        {
            if (string.IsNullOrEmpty(command.LongLink))
            {
                return null;
            }

            string salt = "R#nd0mS@ltV@lue";
            var saltedLongLink = $"{command.LongLink}{salt}";

            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}//{_httpContextAccessor.HttpContext.Request.Host.Value}/";
            LinkGenerator.SetBaseURL(baseUrl);
            HashHelper hashHelper = new HashHelper();
            string hash = hashHelper.GenerateHashUrl(saltedLongLink); //hashHelper.GenerateHashUrl(command.LongLink);
            var shortLink = LinkGenerator.GenerateShortLink(baseUrl, hash);

            var newTinyLink = new Models.TinyLink
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                LongLink = command.LongLink,
                ShortLink = shortLink,
                Hash = hash,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now.AddMonths(1),
                Deleted = false
            };

            _genericRepository.Add(newTinyLink);
            _genericRepository.Save();
            return newTinyLink;
        }

        public Models.TinyLink GetTinyLink(ConnectToTinyLinkQuery query)
        {
            var hash = query.TinyLink.Split('/').Last();
            var tinyLink = _genericRepository.GetByCondition(x => x.Hash == hash).FirstOrDefault();
            return tinyLink;
        }

        public string GetOriginalLink(ConnectToTinyLinkQuery query)
        {
            var hash = query.TinyLink.Split('/').Last();
            var tinyLink = _genericRepository.GetByCondition(x => x.Hash == hash).FirstOrDefault();
            return tinyLink?.LongLink ?? string.Empty;

        }


        public void DeleteTinyLink(Guid id)
        {
            var link = _genericRepository.GetById(id);
            if (link != null)
            {
                link.Deleted = true;
                _genericRepository.Update(link);
                _genericRepository.Save();
            }
        }

        public void UpdateTinyLink(TinyLinkDto dto)
        {
            var link = _genericRepository.GetById(dto.Id);
            if (link is null)
            {
                return;
            }
            link.LongLink = dto.LongLink;
            link.UpdatedDateTime = DateTime.Now;
            link.Deleted = dto.Deactivated;
            _genericRepository.Update(link);
            _genericRepository.Save();
        }

        public IEnumerable<TinyLinkDto> GetAllTinyLinks()
        {
            return _genericRepository.GetAll().Where(x => !x.Deleted).Select(x => x.MapToDto());
        }

        public IEnumerable<TinyLinkDto> GetTinyLinksByUserId(Guid userId)
        {

            return _genericRepository.GetAll().Where(x => !x.Deleted && x.UserId == userId).Select(x => x.MapToDto());
        }
        public IEnumerable<Models.TinyLink> GetTinyLinkByHash(string hash)
        {
            var tinyLink = _genericRepository.GetByCondition(x => x.Hash == hash).FirstOrDefault();
            yield return tinyLink;
        }

    }



}
