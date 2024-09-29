namespace ArcadeVault.Domain.Monads.Result;

public readonly partial record struct Result<TValue>
{
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> binder)
    {
        return IsSuccess
            ? binder(_value)
            : Result<TNewValue>.Error([.. _errors]);
    }

    public async Task<Result<TNewValue>> BindAsync<TNewValue>(
        Func<TValue, Task<Result<TNewValue>>> binder)
    {
        return IsSuccess
            ? await binder(_value)
            : Result<TNewValue>.Error([.. _errors]);
    }
}