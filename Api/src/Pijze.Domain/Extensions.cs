using Microsoft.Extensions.DependencyInjection;
using Pijze.Domain.Services;
using Pijze.Domain.Services.Interfaces;

namespace Pijze.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IBeerService, BeerService>();
        return services;
    }
}