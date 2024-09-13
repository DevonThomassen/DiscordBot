using ArcadeVault.Domain.Common.Enums;
using ArcadeVault.Domain.Games.Coinflip;

namespace ArcadeVault.Web.DTO.CoinFlip;

/// <summary>
/// Simulate coin flip response
/// </summary>
/// <param name="GameResult"></param>
/// <param name="CoinFlipResult"></param>
public readonly record struct SimulateCoinFlipResponse(
    GameResult GameResult,
    CoinFlipOutcome CoinFlipResult);