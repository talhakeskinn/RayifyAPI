using Microsoft.AspNetCore.Mvc;
using Rayify.Application.Abstractions.Storage.Main;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Rayify.Application.Abstractions.Music;
using System.Text.Json;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;

namespace Rayify.API.Controllers
{
    public class UploadController : Controller
    {
        private readonly IStorageService _storageService;
        private readonly IMusicService _musicService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadController(IStorageService storageService, IMusicService musicService, IWebHostEnvironment webHostEnvironment)
        {
            _storageService = storageService;
            _musicService = musicService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(IFormFileCollection files)
        {
            var values = await _storageService.UploadAsync("musics", files);
            return Ok(values);
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
            var youtube = new YoutubeClient();
            var videoUrl = "https://youtube.com/watch?v=u_yIGGhubZs";
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");

            return Ok();
        }
    }
}
