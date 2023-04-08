using Pijze.Application.Breweries.Commands;
using Pijze.Application.Breweries.Queries;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Queries;

namespace Pijze.Api.Apis;

internal class BreweryApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/api/brewery/{id:guid}", Get).RequireAuthorization("read:pijze", "admin");
        app.MapGet("/api/brewery", GetAll).RequireAuthorization("read:pijze", "admin");
        app.MapPost("/api/brewery", Post).RequireAuthorization("read:pijze", "admin");
        app.MapPost("/api/brewery/{id:guid}", Update).RequireAuthorization("read:pijze", "admin");
        app.MapDelete("/api/brewery/{id:guid}", Delete).RequireAuthorization("read:pijze", "admin");
    }
    
    async Task<IResult> Get(Guid id, IQueryDispatcher dispatcher)
    {
        var beer = await dispatcher.QueryAsync(new FindBrewery(id));
        return beer is null ? Results.NotFound() : Results.Ok(beer);
    }  

    async Task<IResult> GetAll(IQueryDispatcher dispatcher)
    {
        return Results.Ok(await dispatcher.QueryAsync(new GetBreweries()));
    }

    async Task<IResult> Post(AddBrewery command, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(command);
        return Results.Ok();
    }

    async Task<IResult> Update(Guid id, UpdateBrewery command, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(command with {Id = id});
        return Results.NoContent();
    }

    async Task<IResult> Delete(Guid id, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(new DeleteBrewery(id));
        return Results.NoContent();
    }
}