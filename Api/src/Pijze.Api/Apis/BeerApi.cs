using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Queries;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Queries;

namespace Pijze.Api.Apis;

internal class BeerApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/api/beer/{id:guid}", Get).RequireAuthorization("read:pijze");
        app.MapGet("/api/beer/{id:guid}/image", GetBeerImage).RequireAuthorization("read:pijze");
        app.MapGet("/api/beer", GetAll).RequireAuthorization("read:pijze");
        app.MapPost("/api/beer", Post).RequireAuthorization("read:pijze", "admin");
        app.MapPost("/api/beer/{id:guid}", Update).RequireAuthorization("read:pijze", "admin");
        app.MapDelete("/api/beer/{id:guid}", Delete).RequireAuthorization("read:pijze", "admin");
    }

    async Task<IResult> Get(Guid id, IQueryDispatcher dispatcher)
    {
        var beer = await dispatcher.QueryAsync(new FindBeer(id));
        return beer is null ? Results.NotFound() : Results.Ok(beer);
    }  
    
    async Task<IResult> GetBeerImage(Guid id, IQueryDispatcher dispatcher)
    {
        var image = await dispatcher.QueryAsync(new GetBeerImage(id));
        return image is null ? Results.NotFound() : Results.File(image, "image/jpeg");
    }

    async Task<IResult> GetAll(IQueryDispatcher dispatcher)
    {
        return Results.Ok(await dispatcher.QueryAsync(new GetBeers()));
    }

    async Task<IResult> Post(AddBeer command, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(command);
        return Results.Ok();
    }

    async Task<IResult> Update(Guid id, UpdateBeer command, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(command with {Id = id});
        return Results.NoContent();
    }

    async Task<IResult> Delete(Guid id, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(new DeleteBeer(id));
        return Results.NoContent();
    }
}