using MediatR;
using ResultPattern.Api.Abstractions;

namespace ResultPattern.Api.Endpoints.Products.GetAll;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new Query();

            var response = await sender.Send(query, cancellationToken);

            return response;
        });
    }
}
