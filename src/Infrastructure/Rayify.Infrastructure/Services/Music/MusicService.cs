using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Rayify.Application.Abstractions.Music;
using Rayify.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Infrastructure.Services.Music
{
    public class MusicService : IMusicService
    {
        private readonly YouTubeService _youtubeService;
        
        public MusicService(IConfiguration configuration)
        {
            BaseClientService.Initializer initializer = new()
            {
                ApiKey = configuration["YoutubeAPI:APIKey"],
                ApplicationName = configuration["YoutubeAPI:ApplicationName"]
            };
            _youtubeService = new(initializer);
        }
        public async Task<List<TrendMusic>> GetTrendMusics(int? maxResult = 50, string? regionCode = "tr")
        {
            var trendsListRequest = _youtubeService.Videos.List("snippet");
            trendsListRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
            trendsListRequest.RegionCode = regionCode;
            trendsListRequest.VideoCategoryId = "10";
            trendsListRequest.MaxResults = maxResult;
            var trendsListResponse = await trendsListRequest.ExecuteAsync();
            List<TrendMusic> trendsList = new();
            foreach (var music in trendsListResponse.Items)
            {
                trendsList.Add(new()
                {
                    Id = music.Id,
                    Title = music.Snippet.Title,
                    Description = music.Snippet.Description,
                    Language = music.Snippet.DefaultAudioLanguage
                });
            }

            return trendsList;

        }
    }
}
