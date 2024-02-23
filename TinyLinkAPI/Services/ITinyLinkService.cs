using TinyLink.API.Commands;
using TinyLink.API.Models;
using TinyLink.API.Queries;

namespace TinyLink.API.Services
{
    public interface ITinyLinkService
    {
        Models.TinyLink CreateTinyLink(CreateTinyLinkCommand command);
        string GetOriginalLink(ConnectToTinyLinkQuery query);
        void DeleteTinyLink(Guid id);
        void UpdateTinyLink(TinyLinkDto dto);
         TinyLinkDto MapToDto(Models.TinyLink link);
        IEnumerable<TinyLinkDto> GetAllTinyLinks();
        IEnumerable<TinyLinkDto> GetTinyLinksByUserId(Guid userId);
    }
}