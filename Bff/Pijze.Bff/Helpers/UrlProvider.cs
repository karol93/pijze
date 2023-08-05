namespace Pijze.Bff.Helpers;

public class UrlProvider
{
    private readonly HttpRequest _request;

    public UrlProvider(HttpRequest request)
    {
        _request = request;
    }
    
    public string GetBaseUrl()
    {
        var schema = _request.Scheme;
        var hostname = Environment.GetEnvironmentVariable(schema == "https" ? "HTTPS_API_BASE_URL" : "HTTP_API_BASE_URL");
        return !string.IsNullOrEmpty(hostname) ? hostname : $"{schema}://{_request.Host.ToString()}";  
    }

}