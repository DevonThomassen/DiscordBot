namespace ArcadeVault.Domain.Monads.ErrorOr;

public readonly partial record struct ErrorOr<TValue>
{
    public ErrorOr<TNewValue> Bind<TNewValue>(Func<TValue, ErrorOr<TNewValue>> binder)
    {
        return IsSuccess
            ? binder(_value)
            : ErrorOr<TNewValue>.Error([.. _errors]);
    }

    public async Task<ErrorOr<TNewValue>> BindAsync<TNewValue>(
        Func<TValue, Task<ErrorOr<TNewValue>>> binder)
    {
        return IsSuccess
            ? await binder(_value)
            : ErrorOr<TNewValue>.Error([.. _errors]);
    }
}