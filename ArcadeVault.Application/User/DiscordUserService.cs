using ArcadeVault.Application.User.Interfaces;
using ArcadeVault.Domain.Common;
using ArcadeVault.Domain.Models;
using ArcadeVault.Domain.Monads.Result;
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
    public Result<bool> IsRegisteredByDiscordId(string discordId)
    {
        return _discordUserRepository.IsRegistered(discordId);
    }

    /// <inheritdoc />
    public Result<DiscordUser> GetUserWithDiscordId(string discordId)
    {
        return _discordUserRepository
            .GetByDiscordId(discordId);
    }

    /// <inheritdoc />
    public async Task<Result<DiscordUser>> RegisterUserWithDiscordAsync(string name, string discordId)
    {
        var isRegistered = _discordUserRepository.IsRegistered(discordId);
        if (isRegistered.IsError)
        {
            return Result<DiscordUser>.Error(isRegistered.FirstError);
        }

        if (isRegistered.Value)
        {
            return Result<DiscordUser>.Error(Error.Conflict(description: "Discord id is already registered"));
        }

        var userResult = await _userRepository.RegisterByNameAsync(name);
        if (userResult.IsError)
        {
            return Result<DiscordUser>.Error(userResult.Errors.FirstOrDefault());
        }

        return await _discordUserRepository.AddDiscordToUserAsync(userResult.Value, discordId);
    }

    /// <inheritdoc />
    public async Task<Result<DomainUser>> UpdateNameAsync(string discordId, string name)
    {
        return await _discordUserRepository.GetUserIdByDiscordId(discordId)
            .BindAsync(id => _userRepository.UpdateNameAsync(id, name));
    }
}