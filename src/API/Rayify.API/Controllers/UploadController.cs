using Microsoft.AspNetCore.Mvc;
using Rayify.Application.Abstractions.Music;
using MediaToolkit.Model;
using MediaToolkit;
using MediatR;
using Rayify.Application.Features.Commands.Music.AddTrends;
using Rayify.Application.Abstractions.YoutubeDownload;
using Rayify.Application.Abstractions.ConvertVideo;

namespace Rayify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMusic _musicService;
        private readonly IYoutubeDownload _youtubeDownload;
        private readonly IMediator _mediator;
        private readonly IConvertVideo _convertVideo;

        public UploadController(IMusic musicService, IWebHostEnvironment webHostEnvironment, IMediator mediator, IYoutubeDownload youtubeDownload, IConvertVideo convertVideo)
        {
            _musicService = musicService;
            _mediator = mediator;
            _youtubeDownload = youtubeDownload;
            _convertVideo = convertVideo;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetYoutubeTrends()
        {
            var trends = await _musicService.GetTrendMusics(50,"tr");
            return Ok(trends);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DownloadVideo()
        {
            
            var trends = await _musicService.GetTrendMusics(11, "tr");
            
            foreach (var music in trends)
            {
                string videoPath = await _youtubeDownload.DownloadMusicAsync(music, "musics/videos");
                string mp3Path = _convertVideo.ConvertToMp3(videoPath, "musics/mp3", music.Id);

                AddTrendsCommandRequest request = new()
                {
                    Title = music.Title,
                    Description = music.Description,
                    Language= music.Language,
                    Published = music.PublishedAt,
                    Path = mp3Path
                };

                await _mediator.Send(request);
            }
            return Ok();
        }
    }
}
