namespace ArcadeVault.Web.DTO.Token;

/// <summary>
/// Claim daily request
/// </summary>
public sealed class ClaimDailyRequest
{
    /// <summary>
    /// Discord ID
    /// </summary>
    public required string DiscordId { get; set; }
}