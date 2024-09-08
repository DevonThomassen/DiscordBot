using DiscordBot.Domain.Models;
using DiscordBot.Domain.Monads.Result;
using DomainUser = DiscordBot.Domain.Models.Common.User;

namespace DiscordBot.Application.User.Interfaces;

public interface IDiscordUserRepository
{
    bool IsRegistered(string discordId);
    Result<DiscordUser> GetByDiscordId(string discordId);
    Result<int> GetUserIdByDiscordId(string discordId);
    Result<bool> HasUserEnoughBalance(string discordId, int tokens);
    Task<Result<DiscordUser>> AddDiscordToUserAsync(DomainUser user, string discordId);
}