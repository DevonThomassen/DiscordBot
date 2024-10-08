﻿using ArcadeVault.Application.Common.Interfaces;
using ArcadeVault.Web.DTO.Common;
using ArcadeVault.Web.DTO.Token;
using ArcadeVault.Web.Extensions.Common;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeVault.Web.Controllers.Common;

/// <summary>
/// Token controller
/// </summary>
/// <param name="tokenService"></param>
[Route("api/[controller]")]
[ApiController]
public class TokenController(ITokenService tokenService) : ControllerBase
{
    private readonly ITokenService _tokenServiceService =
        tokenService ?? throw new ArgumentNullException(nameof(tokenService));

    /// <summary>
    /// Claim daily
    /// </summary>
    /// <param name="requestBody"></param>
    /// <returns></returns>
    [HttpPost("claim-daily")]
    [ProducesResponseType(typeof(BalanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ClaimDaily([FromBody] ClaimDailyRequest requestBody)
    {
        var result = await _tokenServiceService.ClaimDaily(requestBody.DiscordId);
        return result.ToActionResult(success => new OkObjectResult(new BalanceResponse
        {
            Amount = success
        }));
    }
}