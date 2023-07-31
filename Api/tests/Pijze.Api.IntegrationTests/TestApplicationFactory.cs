using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Pijze.Api.IntegrationTests.Security;
using Pijze.Domain.SeedWork;
using Pijze.Infrastructure.Data.EntityFramework.Repositories;

namespace Pijze.Api.IntegrationTests;

internal class TestApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _serviceOverride;

    public TestApplicationFactory(Action<IServiceCollection>? serviceOverride = null)
    {
        _serviceOverride = serviceOverride;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, MockAuthenticationHandler>("Test", null);
         
            // var repositoriesTypes = typeof(BeerRepository).Assembly.GetTypes()
            //     .Where(t => t.GetInterfaces().Contains(typeof(IRepository)))
            //     .ToList();
            //
            // var registeredRepositories = services.Where(x => repositoriesTypes.Contains(x.ImplementationType)).ToList();
            // foreach (var registeredRepo in registeredRepositories)
            //     services.Remove(registeredRepo);
            //
            // foreach (var type in repositoriesTypes)
            // {
            //     foreach (var typeInterface in type.GetInterfaces())
            //     {
            //         var mockType = typeof(Mock<>).MakeGenericType(typeInterface);
            //         var mock = (Mock)Activator.CreateInstance(mockType);
            //         services.AddScoped(typeInterface, x=>mock.Object);
            //     }
            // }
        });
        
        
        if (_serviceOverride is not null)
        {
            builder.ConfigureServices(_serviceOverride);
        }
      
        return base.CreateHost(builder);
    }
}