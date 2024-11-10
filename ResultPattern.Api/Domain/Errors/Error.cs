namespace ResultPattern.Api.Domain.Errors;

/// <summary>
/// Represents the error.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Description">The error description.</param>
public sealed record Error(int Code, string Description)
{
    #region Common

    public static Error Default => new(1, "Default error");

    public static Error BadRequest => new(2, "Bad request");

    public static Error NotFound => new(3, "Not found");

    #endregion

    #region Products

    public static Error ProductNotFound => new(100, "Product not found");

    public static Error ProductBadRequest => new(101, "Product bad request");

    #endregion
}