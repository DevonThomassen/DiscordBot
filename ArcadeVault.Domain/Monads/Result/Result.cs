using ArcadeVault.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace ArcadeVault.Domain.Monads.Result;

public readonly partial record struct Result<TValue>
{
    private readonly TValue? _value;
    private readonly List<Error>? _errors;

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(_errors))]
    [MemberNotNullWhen(false, nameof(Errors))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(_errors))]
    [MemberNotNullWhen(true, nameof(Errors))]
    public bool HasErrors => _errors is not null;

    private Result(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
        IsSuccess = true;
    }

    private Result(TValue value, List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(errors);

        if (errors.Count == 0)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        _value = value;
        _errors = errors;
        IsSuccess = true;
    }

    private Result(Error error)
    {
        _errors = [error];
        IsSuccess = false;
    }

    private Result(List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        if (errors.Count == 0)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        _errors = errors;
        IsSuccess = false;
    }

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Result is not successful.");

    public TValue? ValueOrDefault => IsSuccess
        ? _value
        : default;

    public IReadOnlyList<Error> Errors => HasErrors
        ? _errors.AsReadOnly()
        : throw new InvalidCastException(
            "The Errors property cannot be accessed when no errors have been recorded. Check HasErrors before accessing Errors.");

    public IReadOnlyList<Error> ErrorsOrEmptyList => HasErrors
        ? _errors.AsReadOnly()
        : EmptyError.Instance;

    public Error FirstError => HasErrors
        ? Errors[0]
        : throw new InvalidOperationException();

    public static Result<TValue> Success(TValue result)
        => new(result);

    public static Result<TValue> Success(TValue result, List<Error>? errors)
    {
        return errors is null || errors.Count == 0
            ? new Result<TValue>(result)
            : new Result<TValue>(result, errors);
    }

    public static Result<TValue> Error(Error error)
        => new(error);

    public static Result<TValue> Error(List<Error> errors)
        => new(errors);
}