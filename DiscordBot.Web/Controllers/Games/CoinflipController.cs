using DiscordBot.Application.Games.CoinFlip;
using DiscordBot.Application.Games.CoinFlip.Models;
using DiscordBot.Domain.Games.Coinflip;
using DiscordBot.Web.DTO.CoinFlip;
using DiscordBot.Web.Extensions.Common;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.Web.Controllers.Games;

/// <summary>
/// Coinflip controller
/// </summary>
/// <param name="coinFlipService"></param>
[Route("api/[controller]")]
[ApiController]
public class CoinflipController(ICoinFlipService coinFlipService) : ControllerBase
{
    private readonly ICoinFlipService _coinFlipService =
        coinFlipService ?? throw new ArgumentNullException(nameof(coinFlipService));

    /// <summary>
    /// Flips a coin and returns the outcome.
    /// </summary>
    /// <returns></returns>
    [HttpGet("flip")]
    [ProducesResponseType(typeof(CoinFlipOutcome), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Flip()
    {
        return Ok(_coinFlipService.Flip());
    }

    /// <summary>
    /// Simulates a coin flip game.
    /// </summary>
    /// <param name="requestBody"></param>
    /// <returns></returns>
    [HttpPost("simulate")]
    [ProducesResponseType(typeof(SimulateCoinFlipResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult SimulateCoinFlip([FromBody] CoinFlipExampleRequest requestBody)
    {
        var result = _coinFlipService.SimulateCoinFlip(new CoinFlipSimulationRequest
        {
            CoinFlipOutcome = requestBody.CoinFlipOutcome
        });
        return Ok(new SimulateCoinFlipResponse(
            result.GameResult,
            result.CoinFlipOutcome
        ));
    }

    /// <summary>
    /// Starts a solo coin flip game with a wager.
    /// </summary>
    /// <param name="requestBody"></param>
    /// <returns></returns>
    [HttpPost("solo-game")]
    [ProducesResponseType(typeof(CoinFlipResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SoloCoinFlipGame([FromBody] SoloCoinFlipRequest requestBody)
    {
        var result = await _coinFlipService.PerformWageredCoinFlipAsync(new CoinFlipWagerRequest
        {
            Amount = requestBody.BetAmount,
            DiscordId = requestBody.DiscordId,
            WagerOutcome = requestBody.CoinFlipOutcome
        });
        return result.ToActionResult(success => new OkObjectResult(new CoinFlipResponse
        {
            GameResult = success.GameResult,
            NewBalance = success.NewBalance,
            CoinFlipOutcome = success.CoinFlipOutcome
        }));
    }
}