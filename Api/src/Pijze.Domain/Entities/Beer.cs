using Pijze.Domain.SeedWork;
using Pijze.Domain.ValueObjects;

#pragma warning disable CS8618

namespace Pijze.Domain.Entities;

public class Beer
{
    private Beer()
    {
    }

    private Beer(AggregateId id, string name, Guid breweryId, Rating rating, BeerImage image)
    {
        Id = id;
        SetName(name);
        SetBrewery(breweryId);
        Rating = rating;
        SetImage(image);
        CreationDate = DateTime.UtcNow;
    }

    internal static Beer Create(AggregateId id, string name, AggregateId breweryId, Rating rating, BeerImage image) =>
        new(id, name, breweryId, rating, image);

    public AggregateId Id { get; private set; }
    public string Name { get; private set; }
    public Rating Rating { get; private set; }
    public BeerImage Image { get; private set; }
    public AggregateId BreweryId { get; private set; }
    public DateTime CreationDate { get; private set; }

    internal void Update(string name, AggregateId breweryId, Rating rating, BeerImage image)
    {
        SetName(name);
        SetBrewery(breweryId);
        Rating = rating;
        SetImage(image);
    }

    private void SetBrewery(AggregateId breweryId) => BreweryId = breweryId;

    private void SetName(string name) =>
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;

    private void SetImage(BeerImage image) => Image = image;
}