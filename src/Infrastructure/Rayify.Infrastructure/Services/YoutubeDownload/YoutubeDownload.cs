using Microsoft.AspNetCore.Hosting;
using Rayify.Application.Abstractions.YoutubeDownload;
using Rayify.Application.DTOs;
using Rayify.Domain.Entities;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Rayify.Infrastructure.Services.YoutubeDownload
{
    public class YoutubeDownload : IYoutubeDownload
    {
        private readonly YoutubeClient _client;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public YoutubeDownload(IWebHostEnvironment webHostEnvironment)
        {
            _client = new YoutubeClient();
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> DownloadMusicAsync(TrendMusic music, string path)
        {
            var streamManifest = await _client.Videos.Streams.GetManifestAsync($"https://youtube.com/watch?v={music.Id}");
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var stream = await _client.Videos.Streams.GetAsync(streamInfo);
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, $"{path}/{music.Id}.{streamInfo.Container}");
            await _client.Videos.Streams.DownloadAsync(streamInfo, filePath);

            return filePath;
        }
    }
}
