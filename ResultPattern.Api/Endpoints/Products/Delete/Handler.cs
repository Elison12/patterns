using MediatR;
using Microsoft.EntityFrameworkCore;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Domain.Errors;
using ResultPattern.Api.Models;
using Unit = ResultPattern.Api.Models.Unit;

namespace ResultPattern.Api.Endpoints.Products.Delete;


public sealed record Command(Guid Id) : IRequest<Result<Unit>>;

public sealed class Handler : IRequestHandler<Command, Result<Unit>>
{
    private readonly IApplicationDbContext _dbContext;

    public Handler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result<Unit>.Failure(Error.NotFound);
        }

        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}

