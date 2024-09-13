namespace ArcadeVault.Web.DTO.Users;

/// <summary>
/// Request to register a Discord user.
/// </summary>
public class RegisterDiscordUserRequest : RegisterUserRequest
{
    /// <summary>
    /// The Discord ID of the user
    /// </summary>
    public required string DiscordId { get; set; }
}