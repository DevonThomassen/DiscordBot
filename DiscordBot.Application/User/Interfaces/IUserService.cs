using DiscordBot.Domain.Monads.Result;
using DomainUser = DiscordBot.Domain.Models.Common.User;

namespace DiscordBot.Application.User.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Registers a new user with the specified name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<Result<DomainUser>> RegisterAsync(string name);

    /// <summary>
    /// Retrieves a user by their public id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Result<DomainUser> GetByPublicId(string userId);
}