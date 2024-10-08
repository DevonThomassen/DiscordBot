﻿using ArcadeVault.Application.Games.CoinFlip.Models;
using ArcadeVault.Domain.Monads.ErrorOr;
using DomainCoinFlipOutcome = ArcadeVault.Domain.Games.Coinflip.CoinFlipOutcome;

namespace ArcadeVault.Application.Games.CoinFlip.Interfaces;

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
    Task<ErrorOr<CoinFlipWagerResult>> PerformWageredCoinFlipAsync(
        CoinFlipWagerRequest wagerRequest);
}