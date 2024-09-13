namespace ArcadeVault.Web.DTO.Users;

/// <summary>
/// Request to update a user's name.
/// </summary>
public class UpdateUserNameRequest
{
    /// <summary>
    /// The new name of the user.
    /// </summary>
    public required string Name { get; init; }
}