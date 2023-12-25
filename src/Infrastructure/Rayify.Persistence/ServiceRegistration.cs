using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rayify.Application.Repositories.Music;
using Rayify.Application.Repositories.Singer;
using Rayify.Domain.Entities.Identity;
using Rayify.Persistence.Contexts;
using Rayify.Persistence.Repositories.Music;
using Rayify.Persistence.Repositories.Singer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services) 
        {
            services.AddDbContext<RayifyDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<RayifyDbContext>();
            services.AddScoped<IMusicReadRepository, MusicReadRepository>();
            services.AddScoped<IMusicWriteRepository, MusicWriteRepository>();
            services.AddScoped<ISingerReadRepository, SingerReadRepository>();
            services.AddScoped<ISingerWriteRepository, SingerWriteRepository>();
        }
    }
}
