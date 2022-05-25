using System.Reflection;
using System.Runtime.Serialization;

namespace Pijze.Tests.Shared.Builders;

public class DomainObjectBuilder<T>
{
    protected readonly T Obj;

    public DomainObjectBuilder()
    {
        Obj = (T)FormatterServices.GetUninitializedObject(typeof(T));
    }

    public DomainObjectBuilder<T> WithId(Guid id)
    {
        var field = typeof(T).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
        field!.SetValue(Obj, id);
        return this;
    }

    public T Build()
    {
        return Obj;
    }
}
