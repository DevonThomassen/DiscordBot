using ArcadeVault.Application.User.Interfaces;
using ArcadeVault.Domain.Common;
using ArcadeVault.Domain.Models;
using ArcadeVault.Domain.Monads.ErrorOr;
using DomainUser = ArcadeVault.Domain.Models.Common.User;

namespace ArcadeVault.Application.User;

internal sealed class DiscordUserService(
    IDiscordUserRepository discordUserRepository,
    IUserRepository userRepository) : IDiscordUserService
{
    private readonly IDiscordUserRepository _discordUserRepository =
        discordUserRepository ?? throw new ArgumentNullException(nameof(discordUserRepository));

    private readonly IUserRepository _userRepository =
        userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    /// <inheritdoc />
    public ErrorOr<bool> IsRegisteredByDiscordId(string discordId)
    {
        return _discordUserRepository.IsRegistered(discordId);
    }

    /// <inheritdoc />
    public ErrorOr<DiscordUser> GetUserWithDiscordId(string discordId)
    {
        return _discordUserRepository
            .GetByDiscordId(discordId);
    }

    /// <inheritdoc />
    public async Task<ErrorOr<DiscordUser>> RegisterUserWithDiscordAsync(string name,
        string discordId)
    {
        var isRegistered = _discordUserRepository.IsRegistered(discordId);
        if (isRegistered.IsError)
        {
            return ErrorOr<DiscordUser>.Error(isRegistered.FirstError);
        }

        if (isRegistered.Value)
        {
            return ErrorOr<DiscordUser>.Error(
                Error.Conflict(description: "Discord id is already registered"));
        }

        var userResult = await _userRepository.RegisterByNameAsync(name);
        if (userResult.IsError)
        {
            return ErrorOr<DiscordUser>.Error(userResult.Errors.FirstOrDefault());
        }

        return await _discordUserRepository.AddDiscordToUserAsync(userResult.Value, discordId);
    }

    /// <inheritdoc />
    public async Task<ErrorOr<DomainUser>> UpdateNameAsync(string discordId, string name)
    {
        return await _discordUserRepository.GetUserIdByDiscordId(discordId)
            .BindAsync(id => _userRepository.UpdateNameAsync(id, name));
    }
}