#pragma warning disable CS8618

namespace Pijze.Domain.Entities;

public class Brewery
{
    private Brewery()
    {
    }

    private Brewery(Guid id, string name)
    {
        Id = id == Guid.Empty ? throw new ArgumentException(nameof(id)) : id;
        SetName(name);
        CreationDate = DateTime.UtcNow;
    }
    
    public static Brewery Create(Guid id, string name)
    {
        return new Brewery(id, name);
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreationDate { get; private set; }
    
    public void Update(string name)
    {
        SetName(name);
    }
    
    private void SetName(string name) =>
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
}