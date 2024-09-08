using DiscordBot.Domain.Common;
using DiscordBot.Domain.Monads.Result;
using Microsoft.Data.SqlClient;

namespace DiscordBot.Infrastructure.Common;

internal static class SqlExecuteHelper
{
    public static Result<T> Execute<T>(Func<T> action)
    {
        try
        {
            return Result<T>.Success(action());
        }
        catch (SqlException ex)
        {
            return Result<T>.Error(Error.Unexpected(description: ex.Message));
        }
        catch (Exception ex)
        {
            return Result<T>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    public static Result<T> Execute<T>(Func<T> action, Func<Exception, Error> errorAction)
    {
        try
        {
            return Result<T>.Success(action());
        }
        catch (Exception ex)
        {
            return Result<T>.Error(errorAction(ex));
        }
    }

    public static async Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> action)
    {
        try
        {
            return Result<T>.Success(await action());
        }
        catch (SqlException ex)
        {
            return Result<T>.Error(Error.Unexpected(description: ex.Message));
        }
        catch (Exception ex)
        {
            return Result<T>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    public static async Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> action, Func<Exception, Error> errorAction)
    {
        try
        {
            return Result<T>.Success(await action());
        }
        catch (Exception ex)
        {
            return Result<T>.Error(errorAction(ex));
        }
    }
}