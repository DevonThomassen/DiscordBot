using DomainCoinFlipOutcome = ArcadeVault.Domain.Games.Coinflip.CoinFlipOutcome;

namespace ArcadeVault.Application.Games.CoinFlip.Models;

public readonly record struct CoinFlipWagerRequest(
    string DiscordId,
    int Amount,
    DomainCoinFlipOutcome WagerOutcome);