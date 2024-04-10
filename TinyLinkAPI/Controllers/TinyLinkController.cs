using Microsoft.AspNetCore.Mvc;
using TinyLink.API.Infrastructure;
using TinyLink.API.Services;
using System.Security.Policy;
using TinyLink.API.Commands;
using TinyLink.API.Queries;
using TinyLink.API.Models.DTOs;

namespace TinyLink.API.Controllers
{
    public class TinyLinkController : Controller
    {

        private readonly ITinyLinkService _tinyLinkService;
        public TinyLinkController(ITinyLinkService tinyLinkService)
        {
            _tinyLinkService = tinyLinkService;
        }

        [HttpPost("CreateTinyLink")]
        public async Task<IActionResult> CreateTinyLink([FromBody] CreateTinyLinkCommand command)
        {
            try
            {
                Models.TinyLink _tinyLink = _tinyLinkService.CreateTinyLink(command);
                return Ok(_tinyLink.ShortLink);
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");

            }

        }

        [HttpGet("ConnectToTinyLink")]
        public async Task<IActionResult> ConnectToTinyLink(ConnectToTinyLinkQuery query)
        {

            try
            {
                var link = _tinyLinkService.GetOriginalLink(query);
                return  Ok(link);
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");
            }

        }


        [HttpGet("GetAllTinyLinks")]
        public IActionResult GetAllTinyLinks()
        {
            var links = _tinyLinkService.GetAllTinyLinks();
            return Ok(links);
        }

        [HttpGet]
        [Route("GetTinyLinksByUserId")]
        public IActionResult GetTinyLinksByUserId(UserQuery query)
        {
            var links = _tinyLinkService.GetTinyLinksByUserId(query.Id);
            return Ok(links);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTinyLink(Guid id)
        {
            _tinyLinkService.DeleteTinyLink(id);
            return Ok();
        }

        [HttpPut("UpdateTinyLink")]
        public IActionResult UpdateTinyLink(TinyLinkDto dto)
        {
            _tinyLinkService.UpdateTinyLink(dto);
            return Ok();
        }


    }
}
