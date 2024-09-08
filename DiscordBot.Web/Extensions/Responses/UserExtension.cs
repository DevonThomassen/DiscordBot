using DiscordBot.Domain.Models.Common;
using DiscordBot.Web.DTO.Users;

namespace DiscordBot.Web.Extensions.Responses;

internal static class UserExtension
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse
        {
            PublicId = user.PublicId,
            Name = user.Name,
            Tokens = user.Tokens,
            ClaimLastDaily = user.ClaimLastDaily
        };
    }
}