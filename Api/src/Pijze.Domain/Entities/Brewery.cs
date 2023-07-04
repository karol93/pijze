using Pijze.Domain.Exceptions;
using Pijze.Domain.SeedWork;

#pragma warning disable CS8618

namespace Pijze.Domain.Entities;

public class Brewery
{
    private Brewery()
    {
    }

    private Brewery(AggregateId id, string name)
    {
        Id = id;
        SetName(name);
        CreationDate = DateTime.UtcNow;
    }
    
    public static Brewery Create(AggregateId id, string name)
    {
        return new Brewery(id, name);
    }

    public AggregateId Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreationDate { get; private set; }
    
    public void Update(string name)
    {
        SetName(name);
    }
    
    private void SetName(string name) =>
        Name = string.IsNullOrEmpty(name) ? throw new InvalidBreweryNameException() : name;
}