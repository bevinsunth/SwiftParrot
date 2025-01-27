using System.Net.Http.Headers;
using SwiftParrot.GitHub.Endpoints;

namespace SwiftParrot;


public static class ConfigureServices
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddRefitClients();
        services.ConfigureCaching();
        services.AddSwagger();
    }

    private static void ConfigureCaching(this IServiceCollection services)
    {
        services.AddOutputCache(options =>
        {
            options.AddBasePolicy(builder => 
                builder.Expire(TimeSpan.FromMinutes(10)));
        });
    }

    private static void AddSwagger(this IServiceCollection services)
    {       
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
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

