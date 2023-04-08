using System.Drawing;
using System.Drawing.Imaging;
using Pijze.Domain.Services;
using Pijze.Domain.Services.Interfaces;

namespace Pijze.Infrastructure.Images;

public class ImageService : IImageService
{
    public byte[] Resize(byte[] bytes, int width, int height)
    {
        Image image = Image.FromStream(new MemoryStream(bytes));
        var resized = new Bitmap(image, new Size(width, height));
        using var imageStream = new MemoryStream();
        resized.Save(imageStream, ImageFormat.Jpeg);
        return imageStream.ToArray();
    }
}