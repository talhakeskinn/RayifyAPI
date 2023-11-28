using Rayify.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Features.Queries.Music.GetAllMusics
{
    public class GetAllMusicsQueryResponse
    {
        public List<TrendMusic> Musics { get; set; }
    }
}
