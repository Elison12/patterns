namespace ResultPattern.Api.Models;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price);