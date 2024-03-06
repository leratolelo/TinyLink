namespace TinyLink.API.Services
{
    public interface IVisitService
    {

        void RecordVisit(Models.TinyLink visit);
        IEnumerable<Models.Visit> GetVisitsByLinkId(Guid linkId);
    }
}
