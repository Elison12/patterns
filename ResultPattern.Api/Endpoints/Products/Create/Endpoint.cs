using Mapster;
using MediatR;
using ResultPattern.Api.Abstractions;

namespace ResultPattern.Api.Endpoints.Products.Create;

public sealed record CreateRequest(string Name, string Description, decimal Price);

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/", async (ISender sender, CreateRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<Command>();

            var response = await sender.Send(command, cancellationToken);

            return response.Match(
                x => Results.Ok(x),
                error => Results.BadRequest(error));
        });
    }
}