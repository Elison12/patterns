using Mapster;
using MediatR;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Domain.Errors;

namespace ResultPattern.Api.Endpoints.Products.Update;

public sealed class UpdateEndpoint : IEndpoint
{
    public sealed record UpdateRequest(string Name, string Description, decimal Price);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:guid}", async (ISender sender, Guid id, UpdateRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<Command>() with { Id = id };

            var response = await sender.Send(command, cancellationToken);

            return response.Match(
                x => Results.NoContent(),
                error =>
                {
                    if (error == Error.ProductNotFound)
                    {
                        return Results.NotFound(error);
                    }
                    if (error == Error.ProductBadRequest)
                    {
                        return Results.BadRequest(error);
                    }

                    return Results.StatusCode(500);
                });
        });
    }
}