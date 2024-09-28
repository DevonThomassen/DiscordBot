using ArcadeVault.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace ArcadeVault.Domain.Monads.ErrorOr;

public readonly partial record struct ErrorOr<TValue>
{
    private readonly TValue? _value;
    private readonly List<Error>? _errors;

    private ErrorOr(TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _value = value;
    }

    private ErrorOr(Error error)
    {
        _errors = [error];
    }

    private ErrorOr(List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        if (errors.Count == 0)
        {
            throw new ArgumentNullException();
        }

        _errors = errors;
    }

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(false, nameof(_errors))]
    public bool IsSuccess => _errors is null;

    public bool IsError => _errors is not null;

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Result is not successful.");

    public TValue? ValueOrDefault => IsSuccess
        ? _value
        : default;

    public IReadOnlyList<Error> Errors => !IsSuccess
        ? _errors.AsReadOnly()
        : throw new InvalidCastException(
            "The Errors property cannot be accessed when no errors have been recorded. Check IsError before accessing Errors.");

    public IReadOnlyList<Error> ErrorsOrEmptyList => !IsSuccess
        ? _errors.AsReadOnly()
        : new List<Error>().AsReadOnly();

    public Error FirstError => !IsSuccess
        ? Errors[0]
        : throw new InvalidOperationException();

    public ErrorOr<TValue> AddError(Error error)
    {
        if (!IsSuccess)
        {
            _errors.Add(error);
        }

        return this;
    }

    public static ErrorOr<TValue> Success(TValue result)
        => new(result);

    public static ErrorOr<TValue> Error(Error error)
        => new(error);

    public static ErrorOr<TValue> Error(List<Error> errors)
        => new(errors);
}