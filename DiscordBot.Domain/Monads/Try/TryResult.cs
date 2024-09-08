namespace DiscordBot.Domain.Monads.Try;

public class TryResult<T>
{
    public bool IsSuccess { get; }
    public T Result { get; }
    public Exception Exception { get; }

    protected TryResult(bool isSuccess, T result, Exception exception)
    {
        IsSuccess = isSuccess;
        Result = result;
        Exception = exception;
    }

    public static TryResult<T> Success(T result) => new(true, result, null);
    public static TryResult<T> Failure(Exception exception) => new(false, default, exception);

    public TryResult<TResult> Bind<TResult>(Func<T, TryResult<TResult>> func)
    {
        if (!IsSuccess) return TryResult<TResult>.Failure(Exception);
        try
        {
            return func(Result);
        }
        catch (Exception ex)
        {
            return TryResult<TResult>.Failure(ex);
        }
    }

    public TryResult<TResult> Map<TResult>(Func<T, TResult> func)
    {
        if (!IsSuccess) return TryResult<TResult>.Failure(Exception);
        try
        {
            return TryResult<TResult>.Success(func(Result));
        }
        catch (Exception ex)
        {
            return TryResult<TResult>.Failure(ex);
        }
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Exception, TResult> onFailure)
    {
        return IsSuccess
            ? onSuccess(Result)
            : onFailure(Exception);
    }
}