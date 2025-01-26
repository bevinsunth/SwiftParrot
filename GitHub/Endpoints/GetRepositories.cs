using Microsoft.AspNetCore.Http.HttpResults;
using Refit;

namespace SwiftParrot.GitHub.Endpoints;

public static class GetRepositories
{
    public static void Map(this IEndpointRouteBuilder app)
    {
        app.MapGet("/getRecent", Handle)
        .WithSummary("Get recent repositories of the user based on the updated_at field")
        .Produces<Response>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }


    public record Request(int count);
    //why use record?
    public record Response(Repository[] Repositories);

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.count).GreaterThan(0).LessThan(50);
        }
    }


    //why use cancellationToken?
    private static async Task<IResult> Handle(IRepositoriesClient client, [AsParameters] Request request, CancellationToken cancellationToken)
    {
        var repositories = await client.GetUserRepositories();
        if (repositories.Length == 0)
        {
            return TypedResults.NotFound();
        }
        repositories = repositories.OrderByDescending(r => r.updated_at).Take(request.count).ToArray();
        return TypedResults.Ok(new Response(repositories));
    }

    //TODO: add mediator?

    public record Repository(string updated_at, string created_at, string name, string description, string html_url);

    public interface IRepositoriesClient
    {
        [Get("/user/repos")]
        Task<Repository[]> GetUserRepositories();
    }
}