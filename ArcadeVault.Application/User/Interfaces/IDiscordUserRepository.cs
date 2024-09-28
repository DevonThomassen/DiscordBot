using ArcadeVault.Domain.Models;
using ArcadeVault.Domain.Monads.ErrorOr;
using DomainUser = ArcadeVault.Domain.Models.Common.User;

namespace ArcadeVault.Application.User.Interfaces;

public interface IDiscordUserRepository
{
    ErrorOr<bool> IsRegistered(string discordId);
    ErrorOr<DiscordUser> GetByDiscordId(string discordId);
    ErrorOr<int> GetUserIdByDiscordId(string discordId);
    ErrorOr<bool> HasUserEnoughBalance(string discordId, int tokens);
    Task<ErrorOr<DiscordUser>> AddDiscordToUserAsync(DomainUser user, string discordId);
}