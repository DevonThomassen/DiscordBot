using ArcadeVault.Domain.Models;
using ArcadeVault.Domain.Monads.Result;
using DomainUser = ArcadeVault.Domain.Models.Common.User;

namespace ArcadeVault.Application.User.Interfaces;

public interface IDiscordUserRepository
{
    Result<bool> IsRegistered(string discordId);
    Result<DiscordUser> GetByDiscordId(string discordId);
    Result<int> GetUserIdByDiscordId(string discordId);
    Result<bool> HasUserEnoughBalance(string discordId, int tokens);
    Task<Result<DiscordUser>> AddDiscordToUserAsync(DomainUser user, string discordId);
}