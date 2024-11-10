using MediatR;
using Microsoft.EntityFrameworkCore;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Domain.Products;
using ResultPattern.Api.Models;

namespace ResultPattern.Api.Endpoints.Products.GetAll;

public sealed record Query : IRequest<IEnumerable<ProductResponse>>;

public sealed class Handler
    : IRequestHandler<Query, IEnumerable<ProductResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public Handler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<IEnumerable<ProductResponse>> Handle(
        Query request,
        CancellationToken cancellationToken)
    {
        var products = await _dbContext.Products
            .AsNoTracking()
            .Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Price))
            .ToListAsync(cancellationToken);

        return products;
    }
}