using ArcadeVault.Domain.Models.Common;
using ArcadeVault.Web.DTO.Users;

namespace ArcadeVault.Web.Extensions.Responses;

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