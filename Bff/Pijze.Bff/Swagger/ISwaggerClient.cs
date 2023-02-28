namespace Pijze.Bff.Swagger;

internal interface ISwaggerClient
{
    Task<Stream> GetSwaggerJsonStream(string basePath);
}