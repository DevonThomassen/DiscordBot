using ArcadeVault.Domain.Monads.ErrorOr;
using DomainUser = ArcadeVault.Domain.Models.Common.User;

namespace ArcadeVault.Application.User.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Registers a new user with the specified name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<ErrorOr<DomainUser>> RegisterAsync(string name);

    /// <summary>
    /// Retrieves a user by their public id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    ErrorOr<DomainUser> GetByPublicId(string userId);
}