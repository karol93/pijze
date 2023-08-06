using Pijze.Domain.Services.Interfaces;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Pijze.Infrastructure.Images;

public class ImageService : IImageService
{
    public byte[] Resize(byte[] bytes, int width, int height)
    {
        using var inputStream = new MemoryStream(bytes);
        using var image = Image.Load(inputStream);
    
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(width, height),
            Mode = ResizeMode.Max,
            Sampler = KnownResamplers.Bicubic,
            Compand = true
        }));

        using var imageStream = new MemoryStream();
        image.Save(imageStream, new JpegEncoder { Quality = 80 });
        return imageStream.ToArray();
    }
}