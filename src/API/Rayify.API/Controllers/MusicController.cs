using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rayify.Application.Features.Commands.Music.AddTrends;
using Rayify.Application.Features.Queries.Music.GetAllMusics;
using Rayify.Application.Repositories.Music;

namespace Rayify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MusicController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMusics()
        {
            GetAllMusicsQueryRequest request = new ();
            GetAllMusicsQueryResponse response = await _mediator.Send(request);
                
            return Ok(response);
        }
    }
}
