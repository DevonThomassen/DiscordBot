using ArcadeVault.Domain.Monads.Result;

namespace ArcadeVault.Application.Common.Interfaces;

public interface ITokenService
{
    /// <summary>
    /// Claims a daily reward for a user with the specified Discord ID.
    /// </summary>
    /// <param name="discordId"></param>
    /// <returns></returns>
    Task<Result<int>> ClaimDaily(string discordId);
}