﻿using ResultPattern.Api.Domain.Errors;

namespace ResultPattern.Api.Models;

/// <summary>
/// Represents the result of an operation.
/// Encapsulates either a success value or an error.
/// </summary>
public sealed class Result<T>
{
    public T? Value { get; }

    public Error? Error { get; }

    public bool IsSuccess { get; }

    public bool IsError => !IsSuccess;

    private Result(T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        IsSuccess = true;
        Error = null;
    }

    private Result(Error error)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error), "Error cannot be null.");
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new(error);

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onError)
    {
        return IsSuccess ? onSuccess(Value!) : onError(Error!);
    }
}

/// <summary>
/// Represents a void type, since <see cref="System.Void"/> is not a valid return type in C#.
/// </summary>
public struct Unit
{
    public static readonly Unit Value = new Unit();
}