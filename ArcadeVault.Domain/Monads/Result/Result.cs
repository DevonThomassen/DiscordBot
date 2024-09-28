using ArcadeVault.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace ArcadeVault.Domain.Monads.Result;

public readonly partial record struct Result<TValue>
{
    private readonly TValue? _value;
    private readonly List<Error>? _errors;
    private bool _isSuccess { get; } = false;

    private Result(TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _value = value;
        _isSuccess = true;
    }

    private Result(TValue value, List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(errors);

        if (errors.Count == 0)
        {
            throw new ArgumentNullException();
        }

        _value = value;
        _errors = errors;
        _isSuccess = true;
    }

    private Result(Error error)
    {
        _errors = [error];
        _isSuccess = false;
    }

    private Result(List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        if (errors.Count == 0)
        {
            throw new ArgumentNullException();
        }

        _errors = errors;
        _isSuccess = false;
    }

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(false, nameof(_errors))]
    public bool IsSuccess => _isSuccess;

    [MemberNotNullWhen(true, nameof(_errors))]
    public bool HasErrors => _errors is not null;

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
        return errors is null
            ? new Result<TValue>(result)
            : new Result<TValue>(result, errors);
    }

    public static Result<TValue> Error(Error error)
        => new(error);

    public static Result<TValue> Error(List<Error> errors)
        => new(errors);
}