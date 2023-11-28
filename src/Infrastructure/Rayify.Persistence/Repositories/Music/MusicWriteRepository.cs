using Rayify.Application.Repositories.Music;
using Rayify.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Persistence.Repositories.Music
{
    public class MusicWriteRepository : WriteRepository<Domain.Entities.Music>, IMusicWriteRepository
    {
        public MusicWriteRepository(RayifyDbContext context) : base(context)
        {
        }
    }
}
