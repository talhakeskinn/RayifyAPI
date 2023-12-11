using Rayify.Application.DTOs;

namespace Rayify.Application.Abstractions.Music
{
    public interface IMusicService
    {
        Task<List<TrendMusic>> GetTrendMusics(int? maxResult, string? regionCode);
    }
}
