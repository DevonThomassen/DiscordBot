namespace DiscordBot.Web.DTO.Users;

/// <summary>
/// Request to register a user
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// The name of the user
    /// </summary>
    public required string Name { get; set; }
}