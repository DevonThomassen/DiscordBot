using ArcadeVault.Domain.Common.Enums;
using ArcadeVault.Domain.Games.Coinflip;

namespace ArcadeVault.Application.Games.CoinFlip.Models;

public readonly record struct CoinFlipWagerResult(
    GameResult GameResult,
    CoinFlipOutcome CoinFlipOutcome,
    int NewBalance);