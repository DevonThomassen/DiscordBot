using DiscordBot.Application.User.Interfaces;
using DiscordBot.Web.DTO.Users;
using DiscordBot.Web.Extensions.Common;
using DiscordBot.Web.Extensions.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.Web.Controllers.Common;

/// <summary>
/// Discord user controller
/// </summary>
/// <param name="discordUserService"></param>
[Route("api/[controller]")]
[ApiController]
public class DiscordUserController(IDiscordUserService discordUserService) : ControllerBase
{
    private readonly IDiscordUserService _discordUserService =
        discordUserService ?? throw new ArgumentNullException(nameof(discordUserService));

    /// <summary>
    /// Register a discord user.
    /// </summary>
    /// <param name="registerDiscordUserRequest"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(DiscordUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterWithDiscord(
        [FromBody] RegisterDiscordUserRequest registerDiscordUserRequest)
    {
        var result =
            await _discordUserService.RegisterUserWithDiscordAsync(registerDiscordUserRequest.Name,
                registerDiscordUserRequest.DiscordId);
        return result.ToActionResult(
            success => new OkObjectResult(success.ToDiscordResponse())
        );
    }

    /// <summary>
    /// Change the name of a discord user.
    /// </summary>
    /// <param name="discordId"></param>
    /// <param name="requestBody"></param>
    /// <returns></returns>
    [HttpPatch("{discordId}/name")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateName([FromRoute] string discordId,
        [FromBody] UpdateUserNameRequest requestBody)
    {
        var result = await _discordUserService.UpdateNameAsync(discordId, requestBody.Name);
        return result.ToActionResult(user => new OkObjectResult(user.ToUserResponse()));
    }
}