using MediatR;
using Rayify.Application.DTOs;
using Rayify.Application.Repositories.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Features.Queries.Music.GetAllMusics
{
    public class GetAllMusicsQueryHandler : IRequestHandler<GetAllMusicsQueryRequest, GetAllMusicsQueryResponse>
    {
        private readonly IMusicReadRepository _readRepository;

        public GetAllMusicsQueryHandler(IMusicReadRepository readRepository)
        {
            _readRepository = readRepository;
        }
        public async Task<GetAllMusicsQueryResponse> Handle(GetAllMusicsQueryRequest request, CancellationToken cancellationToken)
        {
            var musics = _readRepository.GetAll();

            List<TrendMusic> trendMusics = new();
            foreach (var music in musics)
            {
                trendMusics.Add(new()
                {
                    Id = music.Id.ToString(),
                    Description = music.Description,
                    Title = music.Title,
                    Language = music.Language,
                    Path = music.Path,
                    PublishedAt = music.Published
                });
            }

            return new GetAllMusicsQueryResponse()
            {
                Musics = trendMusics
            };
           
        }
    }
}
