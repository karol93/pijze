using System.Text.Json;

namespace Pijze.Api.IntegrationTests;

internal static class JsonOptions
{
    public static readonly JsonSerializerOptions Get = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
}