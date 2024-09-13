namespace ArcadeVault.Web.DTO.Users;

/// <summary>
/// User DTO
/// </summary>
/// <param name="PublicId"></param>
/// <param name="Name"></param>
/// <param name="Tokens"></param>
/// <param name="ClaimLastDaily"></param>
public readonly record struct UserResponse(
    string PublicId,
    string Name,
    int Tokens,
    DateOnly? ClaimLastDaily);