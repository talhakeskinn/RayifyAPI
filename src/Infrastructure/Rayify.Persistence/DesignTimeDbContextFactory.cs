using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Rayify.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Persistence
{
    public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<RayifyDbContext>
    {
         public RayifyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<RayifyDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new RayifyDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
