﻿using ArcadeVault.Domain.Models;
using ArcadeVault.Domain.Monads.ErrorOr;
using DomainUser = ArcadeVault.Domain.Models.Common.User;

namespace ArcadeVault.Application.User.Interfaces;

public interface IDiscordUserService
{
    /// <summary>
    /// Checks if a user is registered with the specified Discord ID.
    /// </summary>
    /// <param name="discordId"></param>
    /// <returns></returns>
    ErrorOr<bool> IsRegisteredByDiscordId(string discordId);

    /// <summary>
    /// Retrieves a user by their Discord ID.
    /// </summary>
    /// <param name="discordId"></param>
    /// <returns></returns>
    ErrorOr<DiscordUser> GetUserWithDiscordId(string discordId);

    /// <summary>
    /// Registers a new user with the specified name and Discord ID.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="discordId"></param>
    /// <returns></returns>
    Task<ErrorOr<DiscordUser>> RegisterUserWithDiscordAsync(string name, string discordId);

    /// <summary>
    /// Updates the name of the user with the specified Discord ID.
    /// </summary>
    /// <param name="discordId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<ErrorOr<DomainUser>> UpdateNameAsync(string discordId, string name);
}