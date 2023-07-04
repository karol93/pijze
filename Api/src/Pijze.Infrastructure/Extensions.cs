using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Common.Commands;
using Pijze.Application.Common.Queries;
using Pijze.Domain.SeedWork;
using Pijze.Domain.Services;
using Pijze.Domain.Services.Interfaces;
using Pijze.Infrastructure.Caching;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Data.DbExecutors;
using Pijze.Infrastructure.Data.EntityFramework;
using Pijze.Infrastructure.Data.EntityFramework.Repositories;
using Pijze.Infrastructure.Images;
using Pijze.Infrastructure.Queries;

namespace Pijze.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        var dbExecutorFactory = new SqlLiteExecutorFactory(connectionString);
        services.AddSingleton<IDbExecutorFactory>(dbExecutorFactory);
   
        services.AddDbContext<PijzeDbContext>(c => c.UseSqlite(connectionString));
        services.Scan(scan => scan.FromAssemblyOf<BeerRepository>().AddClasses(classes => classes.AssignableTo<IRepository>()).AsImplementedInterfaces().WithScopedLifetime());
            
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Scan(s => s.FromAssemblyOf<ICommand>()
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblyOf<QueryDispatcher>()
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<>), typeof(CachingCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(CachingQueryHandlerDecorator<,>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IImageService, ImageService>();

        services.AddMemoryCache();
        services.AddSingleton<ICacheStore>(x => new MemoryCacheStore(x.GetRequiredService<IMemoryCache>()));

        services.AddValidatorsFromAssemblyContaining<AddBeer.AddBeerValidator>(includeInternalTypes: true);
        
        return services;
    }
}