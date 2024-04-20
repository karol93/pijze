using Microsoft.AspNetCore.Http.HttpResults;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Queries;

namespace Pijze.Api.Apis;

internal class BeerApi : IApi
{
    private const string GetBeerRouterName = "GetBeer";

    public void Register(WebApplication app)
    {
        var group = app.MapGroup("/api/beer");
        
        group.MapGet("/{id:guid}", Get).RequireAuthorization("read:pijze").WithName(GetBeerRouterName);
        group.MapGet("/{id:guid}/image", GetBeerImage).RequireAuthorization("read:pijze");
        group.MapGet("/", GetAll).RequireAuthorization("read:pijze");
        group.MapPost("/", Create).RequireAuthorization("read:pijze", "admin");
        group.MapPost("/{id:guid}", Update).RequireAuthorization("read:pijze", "admin");
        group.MapDelete("/{id:guid}", Delete).RequireAuthorization("read:pijze", "admin");
    }

    internal async Task<Results<Ok<BeerDto>,NotFound>> Get(Guid id, IQueryDispatcher dispatcher)
    {
        var beer = await dispatcher.QueryAsync(new FindBeer(id));
        return beer is null ? TypedResults.NotFound() : TypedResults.Ok(beer);
    }  
    
    internal async Task<Results<FileContentHttpResult,NotFound>> GetBeerImage(Guid id, IQueryDispatcher dispatcher)
    {
        var image = await dispatcher.QueryAsync(new GetBeerImage(id));
        return image is null ? TypedResults.NotFound() : TypedResults.File(image, "image/jpeg");
    }

    internal async Task<Ok<IEnumerable<BeerListItemDto>>> GetAll(IQueryDispatcher dispatcher)
    {
        return TypedResults.Ok(await dispatcher.QueryAsync(new GetBeers()));
    }

    internal async Task<Results<CreatedAtRoute<BeerDto>,BadRequest<string>>> Create(AddBeer command, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        var id = await commandDispatcher.SendAsync<AddBeer,Guid>(command);
        var beer = await queryDispatcher.QueryAsync(new FindBeer(id));
        return TypedResults.CreatedAtRoute(beer, GetBeerRouterName, new {id});
    }

    internal async Task<Results<NoContent, BadRequest<string>>> Update(Guid id, UpdateBeer command, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(command with {Id = id});
        return TypedResults.NoContent();
    }

    internal async Task<Results<NoContent, BadRequest<string>>> Delete(Guid id, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(new DeleteBeer(id));
        return TypedResults.NoContent();
    }
}