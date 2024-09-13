using ArcadeVault.Domain.Common.Enums;
using DomainCoinFlipOutcome = ArcadeVault.Domain.Games.Coinflip.CoinFlipOutcome;

namespace ArcadeVault.Application.Games.CoinFlip.Models;

public readonly record struct SimulateCoinFlipResult(
    GameResult GameResult,
    DomainCoinFlipOutcome CoinFlipOutcome);