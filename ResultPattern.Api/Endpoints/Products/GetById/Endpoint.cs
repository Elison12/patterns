using MediatR;
using ResultPattern.Api.Abstractions;

namespace ResultPattern.Api.Endpoints.Products.GetById;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var query = new Query(id);

            var response = await sender.Send(query, cancellationToken);

            return response;
        });
    }
}