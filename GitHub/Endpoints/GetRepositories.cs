using Microsoft.AspNetCore.Http.HttpResults;
using Refit;
using SwiftParrot.Common;
using SwiftParrot.Common.Api;

namespace SwiftParrot.GitHub.Endpoints;

public class GetRepositories : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/repository/latest", Handler)
            .WithSummary("Get recent repositories of the user based on the updated_at field")
            .WithRequestValidation<Request>();
    }


    public record Request(string username, int top);

    public record Response(Repository[] Repositories);

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.username).NotEmpty();
            RuleFor(x => x.top).GreaterThan(0).LessThan(50);
        }
    }

    //ToDo: Implement cancellationtoken
    private static async Task<Ok<Response>>  Handler(IRepositoriesClient client, [AsParameters] Request request)
    {
        var repositories = await client.GetUserRepositories(request.username);
        repositories = repositories.OrderByDescending(r => r.updated_at).Take(request.top).ToArray();
        return TypedResults.Ok(new Response(repositories));
    }
    

    public record Repository(string updated_at, string created_at, string name, string description, string html_url);

    public interface IRepositoriesClient
    {
        [Get("/users/{username}/repos")]
        Task<Repository[]> GetUserRepositories(string username);
    }
}