namespace Pijze.Bff.Swagger;

internal class SwaggerClient : ISwaggerClient
{
    private readonly HttpClient _httpClient;

    public SwaggerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Stream> GetSwaggerJsonStream(string basePath)
    {
        return await _httpClient.GetStreamAsync($"{basePath.TrimEnd('/')}/swagger/v1/swagger.json");
    }
}