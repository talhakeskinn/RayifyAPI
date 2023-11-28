using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Application.Features.Commands.Music.AddTrends
{
    public class AddTrendsCommandRequest: IRequest<AddTrendsCommandResponse>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Language { get; set; }
        public string Path { get; set; }
        public DateTime Published { get; set; }

    }
}
