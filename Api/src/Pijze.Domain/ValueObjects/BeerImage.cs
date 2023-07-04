#pragma warning disable CS8618

using Pijze.Domain.Exceptions;
using Pijze.Domain.Services.Interfaces;

namespace Pijze.Domain.ValueObjects;

public record BeerImage
{
    private const int Weight = 600;
    private const int Height = 800;
    
    private BeerImage() { }
    
    private BeerImage(byte[] bytes, IImageService imageService)
    {
        ArgumentNullException.ThrowIfNull(imageService);
        Bytes = imageService.Resize(bytes, Weight, Height);
    }
    
    public static BeerImage Create(string base64Image, IImageService imageService)
    {
        if (!IsValidBase64(base64Image))
            throw new InvalidBase64FormatException();
        return new BeerImage(Convert.FromBase64String(base64Image), imageService);
    }
    
    public byte[] Bytes { get; }
    public string ToBase64() => Convert.ToBase64String(Bytes);
    
    private static bool IsValidBase64(string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return false;
        try
        {
            Convert.FromBase64String(base64String);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}