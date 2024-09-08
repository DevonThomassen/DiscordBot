using DiscordBot.Application.Games.CoinFlip.Models;
using DiscordBot.Domain.Monads.Result;
using DomainCoinFlipOutcome = DiscordBot.Domain.Games.Coinflip.CoinFlipOutcome;

namespace DiscordBot.Application.Games.CoinFlip;

public interface ICoinFlipService
{
    /// <summary>
    /// Flips a coin and returns the result.
    /// </summary>
    /// <returns></returns>
    DomainCoinFlipOutcome Flip();

    /// <summary>
    /// Provides an simulation result of a coin flip operation.
    /// </summary>
    /// <param name="simulationRequest"></param>
    /// <returns></returns>
    SimulateCoinFlipResult SimulateCoinFlip(CoinFlipSimulationRequest simulationRequest);

    /// <summary>
    /// Performs a coin flip with a wager and returns the outcome.
    /// </summary>
    /// <param name="wagerRequest"></param>
    /// <returns></returns>
    Task<Result<CoinFlipWagerResult>> PerformWageredCoinFlipAsync(CoinFlipWagerRequest wagerRequest);
}