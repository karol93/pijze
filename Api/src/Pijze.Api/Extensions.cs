using Pijze.Api.Apis;
using Pijze.Api.Middlewares;

namespace Pijze.Api;

public static class Extensions
{
    public static IServiceCollection AddApis(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IApi>()
            .AddClasses(classes => classes.AssignableTo<IApi>())
            .AsImplementedInterfaces().WithTransientLifetime());
        return services;
    }

    public static WebApplication UseApis(this WebApplication app)
    {
        var apis = app.Services.GetServices<IApi>();
        foreach (var api in apis)
        {
            api.Register(app);
        }

        return app;
    }

    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
}