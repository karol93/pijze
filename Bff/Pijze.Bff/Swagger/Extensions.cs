using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Yarp.ReverseProxy.Configuration;

namespace Pijze.Bff.Swagger;

internal static class Extensions
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ISwaggerClient, SwaggerClient>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = "JWT",
                Flows = new OpenApiOAuthFlows
                {
                    Implicit  = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"https://{configuration["Auth0:Domain"]}/oauth/token"),
                        AuthorizationUrl = new Uri($"https://{configuration["Auth0:Domain"]}/authorize?audience={configuration["Auth0:ApiAudience"]}"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenId" },
                            { "read:pijze", "read:pijze"}
                  
                        }
                    }
                }
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { "openid" }
                }
            });
        });

        return services;
    }
    internal static void MapSwaggers(this IEndpointRouteBuilder endpoints, IConfiguration configuration, ISwaggerClient swaggerHttpClient)
    {
        endpoints.Map("/swagger/services/1.0/swagger.json", async context =>
        {
            var bffSwaggerStream = await swaggerHttpClient.GetSwaggerJsonStream($"{context.Request.Scheme}://{context.Request.Host}");
            var bffSwaggerDocument = new OpenApiStreamReader().Read(bffSwaggerStream, out var diagnostic);
            bffSwaggerDocument.Paths.Clear();
        
            var clusters = configuration.GetSection("ReverseProxy:Clusters").Get<Dictionary<string, ClusterConfig>>();
            if (clusters is null)
            {
                await context.Response.WriteAsync(
                    bffSwaggerDocument.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json)
                );
            }
            else
            {
                foreach (var cluster in clusters)
                {
                    var address = cluster.Value.Destinations.First().Value.Address;
                    var apiSwaggerStream = await swaggerHttpClient.GetSwaggerJsonStream(address);
                    var apiSwaggerDocument = new OpenApiStreamReader().Read(apiSwaggerStream, out var diagnostic2);
                    foreach (var path in apiSwaggerDocument.Paths)
                    {
                        bffSwaggerDocument.Paths.Add(path.Key, path.Value);
                    }
                    foreach (var schema in apiSwaggerDocument.Components.Schemas)
                    {
                        bffSwaggerDocument.Components.Schemas.Add(schema.Key,schema.Value);
                    }
                }
          
                await context.Response.WriteAsync(
                    bffSwaggerDocument.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json)
                );
            }
        });
    }
}