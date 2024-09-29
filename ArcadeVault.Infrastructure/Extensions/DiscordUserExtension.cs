using ArcadeVault.Domain.Models;
using ArcadeVault.Infrastructure.Database.Entities.Discord;

namespace ArcadeVault.Infrastructure.Extensions;

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