using ArcadeVault.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace ArcadeVault.Domain.Monads.ErrorOr;

public readonly partial record struct ErrorOr<TValue>
{
    private readonly TValue? _value;
    private readonly List<Error>? _errors;

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(_errors))]
    [MemberNotNullWhen(false, nameof(Errors))]
    public bool IsSuccess => _errors is null;

    [MemberNotNullWhen(true, nameof(_errors))]
    [MemberNotNullWhen(true, nameof(Errors))]
    public bool IsError => _errors is not null;

    private ErrorOr(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
    }

    private ErrorOr(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
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

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("ErrorOr is not successful.");

    public TValue? ValueOrDefault => IsSuccess
        ? _value
        : default;

    public IReadOnlyList<Error> Errors => !IsSuccess
        ? _errors.AsReadOnly()
        : throw new InvalidCastException(
            "The Errors property cannot be accessed when no errors have been recorded. Check IsError before accessing Errors.");

    public IReadOnlyList<Error> ErrorsOrEmptyList => !IsSuccess
        ? _errors.AsReadOnly()
        : EmptyError.Instance;

    public Error FirstError => !IsSuccess
        ? Errors[0]
        : throw new InvalidOperationException("No errors");

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