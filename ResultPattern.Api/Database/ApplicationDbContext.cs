using Microsoft.EntityFrameworkCore;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Domain.Products;
using System.Reflection;

namespace ResultPattern.Api.Database;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    public DbSet<Product> Products { get; set; }
}