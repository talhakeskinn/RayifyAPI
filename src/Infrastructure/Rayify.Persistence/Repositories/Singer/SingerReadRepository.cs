using Rayify.Application.Repositories.Singer;
using Rayify.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Persistence.Repositories.Singer
{
    public class SingerReadRepository : ReadRepository<Domain.Entities.Singer>, ISingerReadRepository
    {
        public SingerReadRepository(RayifyDbContext context) : base(context)
        {
        }
    }
}
