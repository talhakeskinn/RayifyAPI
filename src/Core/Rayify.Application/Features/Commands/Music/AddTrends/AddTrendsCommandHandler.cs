using MediatR;
using Rayify.Application.Repositories.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Features.Commands.Music.AddTrends
{
    public class AddTrendsCommandHandler : IRequestHandler<AddTrendsCommandRequest, AddTrendsCommandResponse>
    {
        private readonly IMusicWriteRepository _writeRepository;

        public AddTrendsCommandHandler(IMusicWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<AddTrendsCommandResponse> Handle(AddTrendsCommandRequest request, CancellationToken cancellationToken)
        {   
            Guid Id = Guid.NewGuid();
            Domain.Entities.Music music = new()
            {
                Id = Id,
                Title = request.Title,
                Description = request.Description,
                Language = request.Language,
                Path = request.Path,
                Published = request.Published,
            };

            await _writeRepository.AddAsync(music);
            await _writeRepository.SaveAsync();

            return new()
            {
                Music = new()
                {
                    Id = Id.ToString(),
                    Title = music.Title,
                    Language = music.Language,
                    Path = music.Path,
                    Description = music.Description,
                    PublishedAt = music.Published
                }
            };
        }
    }
}
