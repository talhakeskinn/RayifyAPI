using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rayify.Domain.Entities;
using Rayify.Domain.Entities.Base;
using Rayify.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Persistence.Contexts
{
    public class RayifyDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public RayifyDbContext(DbContextOptions options): base(options) 
        {
            
        }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Singer> Singers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet <Album> Albums { get; set; }

        public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach ( var item in datas )
            {
                _ = item.State switch
                {
                    EntityState.Added => item.Entity.Created = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                } ;

            }
            return await base.SaveChangesAsync( cancellationToken );
        }
    }
}
