using MediatR;
using Quartz;
using Rayify.Application.Abstractions.ConvertVideo;
using Rayify.Application.Abstractions.ICronJob;
using Rayify.Application.Abstractions.Music;
using Rayify.Application.Abstractions.YoutubeDownload;
using Rayify.Application.Features.Commands.Music.AddTrends;
using Rayify.Application.Helpers;
using Rayify.Infrastructure.Services.Music;

namespace Rayify.Infrastructure.Services.CronJob
{
    public class CronJob : ICronJob
    {
        private readonly IMusic _musicService;
        private readonly IYoutubeDownload _youtubeDownload;
        private readonly IMediator _mediator;
        private readonly IConvertVideo _convertVideo;

        public CronJob(IMusic musicService, IYoutubeDownload youtubeDownload, IMediator mediator, IConvertVideo convertVideo)
        {
            _convertVideo = convertVideo;
            _mediator = mediator;
            _musicService = musicService;
            _youtubeDownload = youtubeDownload;
        }

        public async Task Execute(IJobExecutionContext context)
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
                    Language = music.Language,
                    Published = music.PublishedAt,
                    Path = mp3Path,
                    SingerName = FindArtist.FromTitle(music.Title),
                };

                await _mediator.Send(request);
                Console.WriteLine($"{music.Id} indirildi.");
            }
        }
    }
}
