using FluentValidation;
using MediatR;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Domain.Errors;
using ResultPattern.Api.Domain.Products;
using ResultPattern.Api.Models;

namespace ResultPattern.Api.Endpoints.Products.Create;

public sealed record Command(string Name, string Description, decimal Price) : IRequest<Result<Guid>>;

public sealed class Handler : IRequestHandler<Command, Result<Guid>>
{

    private readonly IApplicationDbContext _dbContext;
    private readonly IValidator<Command> _validator;

    public Handler(IApplicationDbContext dbContext, IValidator<Command> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Guid>.Failure(Error.ProductBadRequest);
        }

        var product = new Product(
            Guid.NewGuid(),
            DateTime.UtcNow,
            request.Name,
            request.Description,
            request.Price);

        _dbContext.Products.Add(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(product.Id);
    }
}