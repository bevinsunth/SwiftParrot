using Microsoft.AspNetCore.Http.HttpResults;
using Refit;
using SwiftParrot.Common;
using SwiftParrot.Common.Api;

namespace SwiftParrot.GitHub.Endpoints;

public class GetUser : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoint = app.MapGroup("/github/user");
        app.MapGet("/{id}", Handle)
        .WithSummary("Get users public profile information.")
        .WithRequestValidation<Request>()
        .Produces<Response>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }


    public record Request(string id);
    //why use record?
    public record Response(Repository[] Repositories);

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.id).NotEmpty();
        }
    }


    //why use cancellationToken?
    private static async Task<IResult> Handle(IRepositoriesClient client, [AsParameters] Request request, CancellationToken cancellationToken)
    {
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