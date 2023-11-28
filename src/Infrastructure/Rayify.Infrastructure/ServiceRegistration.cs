using Microsoft.Extensions.DependencyInjection;
using Rayify.Application.Abstractions.Music;
using Rayify.Application.Abstractions.Storage.Main;
using Rayify.Application.Abstractions.Token;
using Rayify.Infrastructure.Services.Music;
using Rayify.Infrastructure.Services.Storage;
using Rayify.Infrastructure.Services.Token;

namespace Rayify.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IMusicService, MusicService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}   
