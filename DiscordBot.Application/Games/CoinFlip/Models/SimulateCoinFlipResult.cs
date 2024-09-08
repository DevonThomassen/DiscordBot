using DiscordBot.Domain.Common.Enums;
using DomainCoinFlipOutcome = DiscordBot.Domain.Games.Coinflip.CoinFlipOutcome;

namespace DiscordBot.Application.Games.CoinFlip.Models;

public readonly record struct SimulateCoinFlipResult(
    GameResult GameResult,
    DomainCoinFlipOutcome CoinFlipOutcome);