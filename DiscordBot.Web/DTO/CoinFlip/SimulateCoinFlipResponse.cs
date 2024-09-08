using DiscordBot.Domain.Common.Enums;
using DiscordBot.Domain.Games.Coinflip;

namespace DiscordBot.Web.DTO.CoinFlip;

/// <summary>
/// Simulate coin flip response
/// </summary>
/// <param name="GameResult"></param>
/// <param name="CoinFlipResult"></param>
public readonly record struct SimulateCoinFlipResponse(
    GameResult GameResult,
    CoinFlipOutcome CoinFlipResult);