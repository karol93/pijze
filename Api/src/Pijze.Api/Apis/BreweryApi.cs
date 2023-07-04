using Microsoft.AspNetCore.Http.HttpResults;
using Pijze.Application.Breweries.Commands;
using Pijze.Application.Breweries.Dto;
using Pijze.Application.Breweries.Queries;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Queries;

namespace Pijze.Api.Apis;

internal class BreweryApi : IApi
{
    public void Register(WebApplication app)
    {
        var group = app.MapGroup("/api/brewery");
        
        group.RequireAuthorization("read:pijze", "admin");
        
        group.MapGet("/{id:guid}", Get);
        group.MapGet("/", GetAll);
        group.MapPost("/", Create);
        group.MapPost("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);
    }
    
    internal async Task<Results<Ok<BreweryDto>,NotFound>> Get(Guid id, IQueryDispatcher dispatcher)
    {
        var brewery = await dispatcher.QueryAsync(new FindBrewery(id));
        return brewery is null ?  TypedResults.NotFound() : TypedResults.Ok(brewery);
    }  

    internal async Task<Ok<IEnumerable<BreweryDto>>> GetAll(IQueryDispatcher dispatcher)
    {
        return TypedResults.Ok(await dispatcher.QueryAsync(new GetBreweries()));
    }

    internal async Task<Results<Ok,BadRequest<string>>> Create(AddBrewery command, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(command);
        return TypedResults.Ok();
    }

    internal async Task<NoContent> Update(Guid id, UpdateBrewery command, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(command with {Id = id});
        return TypedResults.NoContent();
    }

    internal async Task<NoContent> Delete(Guid id, ICommandDispatcher dispatcher)
    {
        await dispatcher.SendAsync(new DeleteBrewery(id));
        return TypedResults.NoContent();
    }
}