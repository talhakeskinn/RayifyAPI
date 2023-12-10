using Rayify.Application.DTOs;

namespace Rayify.Application.Abstractions.YoutubeDownload
{
    public interface IYoutubeDownload
    {
        Task<string> DownloadMusicAsync(TrendMusic music, string path);
    }
}
