﻿using ArcadeVault.Domain.Monads.ErrorOr;
using DomainUser = ArcadeVault.Domain.Models.Common.User;

namespace ArcadeVault.Application.User.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Register a user by a name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<ErrorOr<DomainUser>> RegisterByNameAsync(string name);

    /// <summary>
    /// Get a user by its internal id
    /// </summary>
    /// <param name="internalId"></param>
    /// <returns></returns>
    ErrorOr<DomainUser> GetByIdAsync(int internalId);

    /// <summary>
    /// Get a user by its public key
    /// </summary>
    /// <param name="publicId"></param>
    /// <returns></returns>
    ErrorOr<DomainUser> GetByPublicId(string publicId);

    /// <summary>
    /// Update the provided user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<ErrorOr<DomainUser>> UpdateAsync(DomainUser user);

    /// <summary>
    /// Update the name of the provided user by id
    /// </summary>
    /// <param name="publicId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<ErrorOr<DomainUser>> UpdateNameAsync(int publicId, string name);

    /// <summary>
    /// Update the balance of a user
    /// </summary>
    /// <param name="publicId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Task<ErrorOr<int>> UpdateBalanceAsync(int publicId, int amount);

    /// <summary>
    /// Delete a user
    /// </summary>
    /// <returns></returns>
    Task<bool> DeleteUserAsync();
}