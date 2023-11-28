using Rayify.Application.Repositories;
using Rayify.Application.Repositories.Music;
using Rayify.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Persistence.Repositories.Music
{
    public class MusicReadRepository : ReadRepository<Domain.Entities.Music>, IMusicReadRepository
    {
        public MusicReadRepository(RayifyDbContext context) : base(context)
        {
        }
    }
}
