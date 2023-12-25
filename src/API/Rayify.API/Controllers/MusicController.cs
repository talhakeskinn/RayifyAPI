using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rayify.Application.Features.Commands.Music.AddTrends;
using Rayify.Application.Features.Queries.Music.GetAllMusics;
using Rayify.Application.Repositories.Music;
using System.Text.RegularExpressions;

namespace Rayify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MusicController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
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
