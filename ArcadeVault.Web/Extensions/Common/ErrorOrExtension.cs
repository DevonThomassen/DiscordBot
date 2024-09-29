using ArcadeVault.Domain.Monads.ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeVault.Web.Extensions.Common;

internal static class ErrorOrExtension
{
    public static IActionResult ToActionResult<T>(this ErrorOr<T> errorOr,
        Func<T, IActionResult> onSuccess)
    {
        return errorOr.Match(
            onSuccess,
            errors => errors.ToErrorActionResult()
        );
    }

    public static IActionResult ToOkActionResult<T>(this ErrorOr<T> errorOr)
    {
        return errorOr.ToActionResult(tValue =>
            new OkObjectResult(tValue)
        );
    }

    public static IActionResult ToNoContentActionResult<T>(this ErrorOr<T> errorOr)
    {
        return errorOr.ToActionResult(_ =>
            new NoContentResult()
        );
    }
}