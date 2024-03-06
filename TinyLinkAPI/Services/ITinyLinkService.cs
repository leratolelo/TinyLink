using TinyLink.API.Commands;
using TinyLink.API.Models;
using TinyLink.API.Queries;

namespace TinyLink.API.Services
{
    public interface ITinyLinkService
    {
        Models.TinyLink CreateTinyLink(CreateTinyLinkCommand command);
        void DeleteTinyLink(Guid id);
        void UpdateTinyLink(TinyLinkDto dto);
        IEnumerable<TinyLinkDto> GetAllTinyLinks();
        IEnumerable<TinyLinkDto> GetTinyLinksByUserId(Guid userId);
        IEnumerable<Models.TinyLink> GetTinyLinkByHash(string hash);
        Models.TinyLink GetTinyLink(ConnectToTinyLinkQuery query);
        string GetOriginalLink(ConnectToTinyLinkQuery query);
    }
}