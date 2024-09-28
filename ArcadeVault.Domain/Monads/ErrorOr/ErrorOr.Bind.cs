namespace ArcadeVault.Domain.Monads.ErrorOr;

public readonly partial record struct ErrorOr<TValue>
{
    public ErrorOr.ErrorOr<TNewValue> Bind<TNewValue>(Func<TValue, ErrorOr.ErrorOr<TNewValue>> binder)
    {
        return IsSuccess
            ? binder(_value)
            : ErrorOr.ErrorOr<TNewValue>.Error([.. _errors]);
    }

    public async Task<ErrorOr.ErrorOr<TNewValue>> BindAsync<TNewValue>(
        Func<TValue, Task<ErrorOr.ErrorOr<TNewValue>>> binder)
    {
        return IsSuccess
            ? await binder(_value)
            : ErrorOr.ErrorOr<TNewValue>.Error([.. _errors]);
    }
}