using ArcadeVault.Domain.Common;

namespace ArcadeVault.Domain.Monads.Result;

public readonly partial record struct Result<TValue>
{
    public TResultReturn Match<TResultReturn>(
        Func<TValue, TResultReturn> onSuccess,
        Func<IReadOnlyList<Error>, TResultReturn> onFailure)
    {
        return IsSuccess
            ? onSuccess(_value)
            : onFailure(_errors);
    }

    public async Task<TResultReturn> MatchAsync<TResultReturn>(
        Func<TValue, Task<TResultReturn>> onSuccess,
        Func<IReadOnlyList<Error>, Task<TResultReturn>> onFailure)
    {
        return IsSuccess
            ? await onSuccess(_value)
            : await onFailure(_errors);
    }
}