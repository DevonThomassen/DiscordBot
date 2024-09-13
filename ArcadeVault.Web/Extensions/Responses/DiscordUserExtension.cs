using ArcadeVault.Domain.Models;
using ArcadeVault.Web.DTO.Users;

namespace ArcadeVault.Web.Extensions.Responses;

internal static class DiscordUserExtension
{
    public static DiscordUserResponse ToDiscordResponse(this DiscordUser user)
    {
        return new DiscordUserResponse
        {
            DiscordId = user.DiscordId,
            PublicId = user.PublicId,
            Name = user.Name,
            Tokens = user.Tokens,
            ClaimLastDaily = user.ClaimLastDaily
        };
    }
}