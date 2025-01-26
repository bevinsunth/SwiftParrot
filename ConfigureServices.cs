using System.Net.Http.Headers;
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
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
            options.InferSecuritySchemes();
        });
    }
    
    private static void AddRefitClients(this IServiceCollection services)
    {
        services.AddRefitClient<GetRepositories.IRepositoriesClient>()
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.github.com");
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("SwiftParrot", "1.0.0"));
                
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            });
    }
}

