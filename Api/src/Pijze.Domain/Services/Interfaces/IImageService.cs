namespace Pijze.Domain.Services.Interfaces;

public interface IImageService
{
    byte[] Resize(byte[] bytes, int weight, int height);
}