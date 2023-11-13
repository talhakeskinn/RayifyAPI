using Microsoft.Extensions.DependencyInjection;
using Rayify.Application.Abstractions.Token;
using Rayify.Infrastructure.Services.Token;

namespace Rayify.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();

        }
    }
}
