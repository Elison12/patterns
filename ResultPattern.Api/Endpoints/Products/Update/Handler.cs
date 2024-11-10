using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Domain.Errors;
using ResultPattern.Api.Models;
using Unit = ResultPattern.Api.Models.Unit;

namespace ResultPattern.Api.Endpoints.Products.Update;

public sealed record Command(Guid Id, string Name, string Description, decimal Price) : IRequest<Result<Unit>>;

public sealed class Handler : IRequestHandler<Command, Result<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IValidator<Command> _validator;

    public Handler(IApplicationDbContext dbContext, IValidator<Command> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Unit>.Failure(Error.ProductBadRequest);
        }

        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result<Unit>.Failure(Error.ProductNotFound);
        }

        product.Update(request.Name, request.Description, request.Price);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}