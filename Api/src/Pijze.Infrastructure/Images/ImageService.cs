using Pijze.Domain.Services.Interfaces;
using SkiaSharp;

namespace Pijze.Infrastructure.Images;

public class ImageService : IImageService
{
    public byte[] Resize(byte[] bytes, int width, int height)
    {
        using var inputStream = new MemoryStream(bytes);
        using var image = SKBitmap.Decode(inputStream);
        using var resizedBitmap = image.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
        using var imageStream = new MemoryStream();
        resizedBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(imageStream);
        return imageStream.ToArray();
    }
}