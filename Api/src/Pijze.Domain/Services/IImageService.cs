namespace Pijze.Domain.Services;

public interface IImageService
{
    byte[] Resize(byte[] bytes, int weight, int height);
}