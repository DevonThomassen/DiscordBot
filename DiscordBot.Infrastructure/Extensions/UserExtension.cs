using DiscordBot.Domain.Models.Common;
using DiscordBot.Infrastructure.Entities.Common;

namespace DiscordBot.Infrastructure.Extensions;

internal static class UserExtension
{
    public static User ToDomain(this UserEntity entity)
    {
        return new User
        {
            InternalId = entity.UserId,
            PublicId = entity.PublicId,
            Name = entity.Name,
            Tokens = entity.Tokens,
            ClaimLastDaily = entity.ClaimLastDaily
        };
    }
}