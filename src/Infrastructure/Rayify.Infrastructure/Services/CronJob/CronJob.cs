using MediatR;
using Quartz;
using Rayify.Application.Abstractions.ConvertVideo;
using Rayify.Application.Abstractions.ICronJob;
using Rayify.Application.Abstractions.Music;
using Rayify.Application.Abstractions.YoutubeDownload;
using Rayify.Application.Features.Commands.Music.AddTrends;
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

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Talhaya götten");
            return Task.FromResult(true);
        }
    }
}
