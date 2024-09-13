using ArcadeVault.Domain.Monads.Result;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeVault.Web.Extensions.Common;

internal static class ResultExtension
{
    public static IActionResult ToActionResult<T>(this Result<T> result, Func<T, IActionResult> onSuccess)
    {
        return result.Match(
            onSuccess,
            errors => errors.ToErrorActionResult()
        );
    }

    public static IActionResult ToOkActionResult<T>(this Result<T> result)
    {
        return result.ToActionResult(tValue =>
            new OkObjectResult(tValue)
        );
    }

    public static IActionResult ToNoContentActionResult<T>(this Result<T> result)
    {
        return result.ToActionResult(_ =>
            new NoContentResult()
        );
    }
}