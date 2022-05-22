#pragma warning disable CS8618

namespace Pijze.Domain.Beers;

public class Beer
{
    private Beer()
    {
    }

    private Beer(Guid id, string name, string manufacturer, int rating, BeerImage image)
    {
        Id = id == Guid.Empty ? throw new ArgumentException(nameof(id)) : id;
        SetName(name);
        SetManufacturer(manufacturer);
        SetRating(rating);
        SetImage(image);
        CreationDate = DateTime.UtcNow;
    }


    public static Beer Create(Guid id, string name, string manufacturer, int rating, BeerImage image)
    {
        return new Beer(id, name, manufacturer, rating, image);
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Manufacturer { get; private set; }
    public int Rating { get; private set; }
    public BeerImage Image { get; private set; }
    public DateTime CreationDate { get; private set; }

    public void Update(string name, string manufacturer, int rating, BeerImage image)
    {
        SetName(name);
        SetManufacturer(manufacturer);
        SetRating(rating);
        SetImage(image);
    }

    private void SetRating(int rating) =>
        Rating = rating < 1 || rating > 5 ? throw new ArgumentOutOfRangeException(nameof(rating)) : rating;

    private void SetManufacturer(string manufacturer) => Manufacturer = string.IsNullOrEmpty(manufacturer)
        ? throw new ArgumentNullException(nameof(manufacturer))
        : manufacturer;

    private void SetName(string name) =>
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;

    private void SetImage(BeerImage image) => Image = image ?? throw new ArgumentNullException(nameof(image));
}