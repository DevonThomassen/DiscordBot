namespace DiscordBot.Web.DTO.Users;

/// <summary>
/// Response containing information about a Discord user
/// </summary>
/// <param name="DiscordId">The Discord ID of the user</param>
/// <param name="PublicId">The public ID of the user</param>
/// <param name="Name">The display name of the user</param>
/// <param name="Tokens">The current token balance of the user</param>
/// <param name="ClaimLastDaily">The date of the user's last daily claim</param>
public readonly record struct DiscordUserResponse(
    string DiscordId,
    string PublicId,
    string Name,
    int Tokens,
    DateOnly? ClaimLastDaily);