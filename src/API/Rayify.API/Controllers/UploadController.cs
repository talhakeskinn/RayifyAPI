using Microsoft.AspNetCore.Mvc;
using Rayify.Application.Abstractions.Storage.Main;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Rayify.Application.Abstractions.Music;
using System.Text.Json;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;
using MediaToolkit.Model;
using MediaToolkit;
using System.Text.RegularExpressions;
using MediatR;
using Rayify.Application.Features.Commands.Music.AddTrends;

namespace Rayify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediator _mediator;

        public UploadController(IMusicService musicService, IWebHostEnvironment webHostEnvironment, IMediator mediator)
        {
            _musicService = musicService;
            _webHostEnvironment = webHostEnvironment;
            _mediator = mediator;
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
            
            var trends = await _musicService.GetTrendMusics(10, "tr");
            var youtube = new YoutubeClient();
            Regex regex = new("[*'\",+-._&#^@|/<>~]");
            foreach (var music in trends)
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync($"https://youtube.com/watch?v={music.Id}");
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                string videoPath = Path.Combine(_webHostEnvironment.WebRootPath, $"musics/videos/{music.Id}.{streamInfo.Container}");
                string musicPath = Path.Combine(_webHostEnvironment.WebRootPath, $"musics/mp3/{music.Id}.mp3");
                await youtube.Videos.Streams.DownloadAsync(streamInfo, videoPath);
                var inputFile = new MediaFile(videoPath);
                var outputFile = new MediaFile(musicPath);
                using (var engine = new Engine())
                {
                    engine.Convert(inputFile, outputFile);
                }

                AddTrendsCommandRequest request = new()
                {
                    Title = music.Title,
                    Description = music.Description,
                    Language= music.Language,
                    Published = music.PublishedAt,
                    Path = $"musics/mp3/{music.Id}.mp3"
                };

                await _mediator.Send(request);
            }
            return Ok();
        }
    }
}
