using ArcadeVault.Domain.Games.Coinflip;

namespace ArcadeVault.Web.DTO.CoinFlip;

/// <summary>
/// Represents a request to test a coin flip game
/// </summary>
public class CoinFlipExampleRequest
{
    /// <summary>
    /// User's choice for the coin flip outcome.
    /// </summary>
    public required CoinFlipOutcome CoinFlipOutcome { get; init; }
}