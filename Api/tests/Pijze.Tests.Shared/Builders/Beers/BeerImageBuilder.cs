using System.Reflection;
using Pijze.Domain.Beers;

namespace Pijze.Tests.Shared.Builders.Beers;

public class BeerImageBuilder : DomainObjectBuilder<BeerImage>
{
    public BeerImageBuilder WithBytes(byte[] bytes)
    {
        var field = typeof(BeerImage).GetField("<Bytes>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic);
        field!.SetValue(Obj, bytes);
        return this;
    }
}