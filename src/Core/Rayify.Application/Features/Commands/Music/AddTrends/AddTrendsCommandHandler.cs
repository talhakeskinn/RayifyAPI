using MediatR;
using Rayify.Application.Repositories.Music;
namespace Rayify.Application.Features.Commands.Music.AddTrends
{
    public class AddTrendsCommandHandler : IRequestHandler<AddTrendsCommandRequest, AddTrendsCommandResponse>
    {
        private readonly IMusicWriteRepository _writeRepository;
        private readonly IMusicReadRepository _readRepository;

        public AddTrendsCommandHandler(IMusicWriteRepository writeRepository, IMusicReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
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
