namespace DiscordBot.Web.DTO.CoinFlip;

/// <summary>
/// Represents a request to perform a coin flip in a solo game.
/// </summary>
public class SoloCoinFlipRequest : CoinFlipExampleRequest
{
    /// <summary>
    /// The unique identifier for the user on Discord.
    /// </summary>
    public required string DiscordId { get; init; }

    /// <summary>
    /// Amount of money the user is betting on the coin flip.
    /// </summary>
    public required int BetAmount { get; init; }
}