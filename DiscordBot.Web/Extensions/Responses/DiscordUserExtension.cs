using DiscordBot.Domain.Models;
using DiscordBot.Web.DTO.Users;

namespace DiscordBot.Web.Extensions.Responses;

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