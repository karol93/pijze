#pragma warning disable CS8618

using Pijze.Domain.Services;

namespace Pijze.Domain.Beers;

public class BeerImage
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
    
    public byte[] Bytes { get; private set; }
    public string ToBase64() => Convert.ToBase64String(Bytes);
}