using ArcadeVault.Domain.Games.Coinflip;

namespace ArcadeVault.Application.Games.CoinFlip.Models;

public readonly record struct CoinFlipSimulationRequest(
    CoinFlipOutcome CoinFlipOutcome);