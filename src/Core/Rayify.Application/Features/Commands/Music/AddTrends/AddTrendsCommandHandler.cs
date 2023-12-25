using MediatR;
using Rayify.Application.Helpers;
using Rayify.Application.Repositories.Music;
using Rayify.Application.Repositories.Singer;
using Rayify.Domain.Entities;

namespace Rayify.Application.Features.Commands.Music.AddTrends
{
    public class AddTrendsCommandHandler : IRequestHandler<AddTrendsCommandRequest, AddTrendsCommandResponse>
    {
        private readonly IMusicWriteRepository _writeRepository;
        private readonly IMusicReadRepository _readRepository;
        private readonly ISingerReadRepository _singerReadRepository;
        private readonly ISingerWriteRepository _singerWriteRepository;

        public AddTrendsCommandHandler(IMusicWriteRepository writeRepository, IMusicReadRepository readRepository, ISingerWriteRepository singerWriteRepository,ISingerReadRepository singerReadRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _singerWriteRepository = singerWriteRepository;
            _singerReadRepository = singerReadRepository;
        }

        public async Task<AddTrendsCommandResponse> Handle(AddTrendsCommandRequest request, CancellationToken cancellationToken)
        {   
            var music = _readRepository.GetWhere(m => m.Path ==  request.Path).FirstOrDefault();
            if (music == null)
            {
                
                Guid Id = Guid.NewGuid();
                Domain.Entities.Music newMusic = new()
                {
                    Id = Id,
                    Title = request.Title,
                    Description = request.Description,
                    Language = request.Language,
                    Path = request.Path,
                    Published = request.Published,
                };

                var singer = _singerReadRepository.GetWhere(s => s.Slug == Slugify.GenerateSlug(request.SingerName)).FirstOrDefault();
                if (singer == null)
                {
                    Singer newSinger = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = request.SingerName
                    };
                    newMusic.Singers = new List<Singer>() { newSinger };
                }
                else
                {
                    newMusic.Singers = new List<Singer>() { singer };
                }

                await _writeRepository.AddAsync(newMusic);
                await _writeRepository.SaveAsync();

                return new()
                {
                    Music = new()
                    {
                        Id = Id.ToString(),
                        Title = newMusic.Title,
                        Language = newMusic.Language,
                        Path = newMusic.Path,
                        Description = newMusic.Description,
                        PublishedAt = newMusic.Published
                    }
                };
            }
            return new();

        }
    }
}
