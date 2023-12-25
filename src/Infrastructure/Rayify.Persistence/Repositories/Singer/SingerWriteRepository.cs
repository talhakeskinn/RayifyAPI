using Rayify.Application.Repositories.Singer;
using Rayify.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Persistence.Repositories.Singer
{
    public class SingerWriteRepository : WriteRepository<Domain.Entities.Singer>, ISingerWriteRepository
    {
        public SingerWriteRepository(RayifyDbContext context) : base(context)
        {
        }
    }
}
