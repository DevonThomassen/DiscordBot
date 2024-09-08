namespace DiscordBot.Web.DTO.Common;

/// <summary>
/// Balance response
/// </summary>
public readonly record struct BalanceResponse
{
    /// <summary>
    /// The amount of tokens
    /// </summary>
    public required int Amount { get; init; }
}