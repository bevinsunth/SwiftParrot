using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.SwaggerGen;
using SwiftParrot.GitHub.Endpoints;

namespace SwiftParrot;


public static class ConfigureServices
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRefitClients();
        builder.AddSwagger();
    }
    
    private static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    
    private static void AddRefitClients(this IServiceCollection services)
    {
        services.AddRefitClient<GetRepositories.IRepositoriesClient>()
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.github.com");
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("SwiftParrot", "1.0.0"));
            });
    }
    
    private static void ConfigureJsonOptions(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options => {
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.IncludeFields = true;
        });
    }
}

