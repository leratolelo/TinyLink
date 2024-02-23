using Microsoft.AspNetCore.Mvc;

namespace TinyLink.API.Queries
{
    public class ConnectToTinyLinkQuery
    {
        [FromQuery]
        public string TinyLink { get; set; }
    }
}
