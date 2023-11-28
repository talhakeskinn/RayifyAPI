using Rayify.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Abstractions.Music
{
    public interface IMusicService
    {
        Task<List<TrendMusic>> GetTrendMusics(int? maxResult, string? regionCode);
    }
}
