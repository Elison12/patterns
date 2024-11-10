using Microsoft.EntityFrameworkCore;
using ResultPattern.Api.Domain.Products;

namespace ResultPattern.Api.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}