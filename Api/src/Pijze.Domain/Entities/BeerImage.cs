#pragma warning disable CS8618

using Pijze.Domain.Services.Interfaces;

namespace Pijze.Domain.Entities;

public class BeerImage : IEquatable<BeerImage>
{
    private const int Weight = 600;
    private const int Height = 800;
    
    private BeerImage() { }
    
    private BeerImage(byte[] bytes, IImageService imageService)
    {
        Bytes = imageService.Resize(bytes, Weight, Height);
    }
    
    public static BeerImage Create(string base64Image, IImageService imageService)
    {
        if (string.IsNullOrEmpty(base64Image))
            throw new ArgumentNullException(nameof(base64Image));
        return new BeerImage(Convert.FromBase64String(base64Image), imageService);
    }
    
    public byte[] Bytes { get; }
    public string ToBase64() => Convert.ToBase64String(Bytes);

    public bool Equals(BeerImage? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Bytes.Equals(other.Bytes);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((BeerImage) obj);
    }

    public override int GetHashCode() => Bytes.GetHashCode();
}