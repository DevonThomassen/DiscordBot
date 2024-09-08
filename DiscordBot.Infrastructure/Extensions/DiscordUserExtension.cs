using DiscordBot.Domain.Models;
using DiscordBot.Infrastructure.Entities.Discord;

namespace DiscordBot.Infrastructure.Extensions;

internal static class DiscordUserExtension
{
    public static DiscordUser ToDomain(this DiscordUserEntity entity)
    {
        return new DiscordUser
        {
            InternalId = entity.UserId,
            DiscordId = entity.DiscordId,
            PublicId = entity.User!.PublicId,
            Name = entity.User!.Name,
            Tokens = entity.User!.Tokens,
            ClaimLastDaily = entity.User.ClaimLastDaily
        };
    }
}