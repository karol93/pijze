using System.Reflection;
using Pijze.Domain.Beers;

namespace Pijze.Tests.Shared.Builders.Beers;

public class BeerBuilder : DomainObjectBuilder<Beer>
{
    public BeerBuilder WithName(string name)
    {
        PropertyInfo property = typeof(Beer).GetProperty(nameof(Beer.Name))!;
        property!.SetValue(Obj, name);
        return this;
    }
    
    public BeerBuilder WithManufacturer(string manufacturer)
    {
        PropertyInfo property = typeof(Beer).GetProperty(nameof(Beer.Manufacturer))!;
        property!.SetValue(Obj, manufacturer);
        return this;
    }
    
    public BeerBuilder WithRating(int rating)
    {
        PropertyInfo property = typeof(Beer).GetProperty(nameof(Beer.Rating))!;
        property!.SetValue(Obj, rating);
        return this;
    }
    
    public BeerBuilder WithImage(BeerImage image)
    {
        PropertyInfo property = typeof(Beer).GetProperty(nameof(Beer.Image))!;
        property!.SetValue(Obj, image);
        return this;
    }
}