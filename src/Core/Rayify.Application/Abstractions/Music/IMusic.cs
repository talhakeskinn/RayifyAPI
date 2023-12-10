using Rayify.Application.DTOs;

namespace Rayify.Application.Abstractions.Music
{
    public interface IMusic
    {
        Task<List<TrendMusic>> GetTrendMusics(int? maxResult, string? regionCode);
    }
}
