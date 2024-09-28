using ArcadeVault.Domain.Common;
using ArcadeVault.Domain.Monads.ErrorOr;
using Microsoft.Data.SqlClient;

namespace ArcadeVault.Infrastructure.Common;

internal static class SqlExecuteHelper
{
    public static ErrorOr<T> Execute<T>(Func<T> action)
    {
        try
        {
            return ErrorOr<T>.Success(action());
        }
        catch (SqlException ex)
        {
            return ErrorOr<T>.Error(Error.Unexpected(description: ex.Message));
        }
        catch (Exception ex)
        {
            return ErrorOr<T>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    public static ErrorOr<T> Execute<T>(Func<T> action, Func<Exception, Error> errorAction)
    {
        try
        {
            return ErrorOr<T>.Success(action());
        }
        catch (Exception ex)
        {
            return ErrorOr<T>.Error(errorAction(ex));
        }
    }

    public static async Task<ErrorOr<T>> ExecuteAsync<T>(Func<Task<T>> action)
    {
        try
        {
            return ErrorOr<T>.Success(await action());
        }
        catch (SqlException ex)
        {
            return ErrorOr<T>.Error(Error.Unexpected(description: ex.Message));
        }
        catch (Exception ex)
        {
            return ErrorOr<T>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    public static async Task<ErrorOr<T>> ExecuteAsync<T>(Func<Task<T>> action,
        Func<Exception, Error> errorAction)
    {
        try
        {
            return ErrorOr<T>.Success(await action());
        }
        catch (Exception ex)
        {
            return ErrorOr<T>.Error(errorAction(ex));
        }
    }
}