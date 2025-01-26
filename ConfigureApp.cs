using SwiftParrot.GitHub.Endpoints;

namespace SwiftParrot
{
    public static class ConfigureApp
    {
        public static void Configure(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.MapEndpoints();
        }
        
        private static void MapEndpoints(this WebApplication app)
        {
            GetRepositories.Map(app);
        }
    }
}
