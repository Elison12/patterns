using MediatR;
using ResultPattern.Api.Abstractions;

namespace ResultPattern.Api.Endpoints.Products.Delete;

public sealed class DeleteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var command = new Command(id);

            var response = await sender.Send(command, cancellationToken);

            return response.Match(
                x => Results.NoContent(),
                error => Results.NotFound(error));
        });
    }
}
