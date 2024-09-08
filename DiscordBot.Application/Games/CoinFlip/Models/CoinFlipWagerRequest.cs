using DomainCoinFlipOutcome = DiscordBot.Domain.Games.Coinflip.CoinFlipOutcome;

namespace DiscordBot.Application.Games.CoinFlip.Models;

public readonly record struct CoinFlipWagerRequest(
    string DiscordId,
    int Amount,
    DomainCoinFlipOutcome WagerOutcome);