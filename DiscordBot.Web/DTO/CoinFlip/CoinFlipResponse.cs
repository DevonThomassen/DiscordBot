using DiscordBot.Domain.Common.Enums;
using DiscordBot.Domain.Games.Coinflip;

namespace DiscordBot.Web.DTO.CoinFlip;

/// <summary>
/// Response of a conflip game with a wager.
/// </summary>
/// <param name="GameResult">The result of the game, indicating whether the player won or lost.</param>
/// <param name="CoinFlipOutcome">The outcome of the coin flip, indicating heads or tails.</param>
/// <param name="NewBalance">The user's new balance after the wager.</param>
public readonly record struct CoinFlipResponse(
    GameResult GameResult,
    CoinFlipOutcome CoinFlipOutcome,
    int NewBalance);