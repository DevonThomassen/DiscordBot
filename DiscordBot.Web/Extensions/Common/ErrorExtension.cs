using DiscordBot.Domain.Common;
using DiscordBot.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.Web.Extensions.Common;

internal static class ErrorExtension
{
    public static IActionResult ToErrorActionResult(this IEnumerable<Error> errors)
    {
        var enumerable = errors as Error[] ?? errors.ToArray();

        if (enumerable.IsNullOrEmpty())
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        var firstError = enumerable[0];

        return firstError.Type switch
        {
            ErrorType.Conflict => new ConflictObjectResult(firstError.Description),
            ErrorType.Failure => new BadRequestObjectResult(firstError.Description),
            ErrorType.Forbidden => new StatusCodeResult(StatusCodes.Status403Forbidden),
            ErrorType.NotFound => new NotFoundObjectResult(firstError.Description),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(firstError.Description),
            ErrorType.Unexpected => new StatusCodeResult(StatusCodes.Status500InternalServerError),
            ErrorType.Validation => enumerable.ToUnprocessableEntityActionResult(),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };
    }

    private static IActionResult ToUnprocessableEntityActionResult(this Error[] errors)
    {
        var validationErrors = errors
            .Where(error => error.Type == ErrorType.Validation);

        return new UnprocessableEntityObjectResult(validationErrors);
    }
}