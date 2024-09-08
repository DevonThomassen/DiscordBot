using DiscordBot.Domain.Common.Enums;
using DiscordBot.Domain.Games.Coinflip;

namespace DiscordBot.Application.Games.CoinFlip.Models;

public readonly record struct CoinFlipWagerResult(
    GameResult GameResult,
    CoinFlipOutcome CoinFlipOutcome,
    int NewBalance);