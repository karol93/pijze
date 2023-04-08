using Pijze.Domain.ValueObjects;

#pragma warning disable CS8618

namespace Pijze.Domain.Entities;

public class Beer
{
    private Beer()
    {
    }

    private Beer(Guid id, string name, Guid breweryId, Rating rating, BeerImage image)
    {
        Id = id == Guid.Empty ? throw new ArgumentException(nameof(id)) : id;
        SetName(name);
        SetBrewery(breweryId);
        Rating = rating;
        SetImage(image);
        CreationDate = DateTime.UtcNow;
    }

    internal static Beer Create(Guid id, string name, Guid breweryId, Rating rating, BeerImage image) =>
        new(id, name, breweryId, rating, image);

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Rating Rating { get; private set; }
    public BeerImage Image { get; private set; }
    public Guid BreweryId { get; private set; }
    public DateTime CreationDate { get; private set; }

    internal void Update(string name, Guid breweryId, Rating rating, BeerImage image)
    {
        SetName(name);
        SetBrewery(breweryId);
        Rating = rating;
        SetImage(image);
    }

    private void SetBrewery(Guid breweryId) => BreweryId = breweryId;

    private void SetName(string name) =>
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;

    private void SetImage(BeerImage image) => Image = image;
}