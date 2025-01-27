using SwiftParrot.GitHub.Endpoints;
using SwiftParrot.Common.Api;

namespace SwiftParrot
{
    public static class ConfigureApp
    {
        public static void Configure(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseOutputCache();
            app.MapEndpoints();
        }
        
        private static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("github");
            endpoints.MapEndpoint<GetRepositories>();
        }
        
    }
}
