using MediatR;
using Microsoft.EntityFrameworkCore;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Models;

namespace ResultPattern.Api.Endpoints.Products.GetById;

public sealed record Query(Guid Id) : IRequest<ProductResponse?>;

public sealed class Handler : IRequestHandler<Query, ProductResponse?>
{
    private readonly IApplicationDbContext _dbContext;

    public Handler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<ProductResponse?> Handle(Query request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Price))
            .FirstOrDefaultAsync(cancellationToken);

        return product;
    }
}