using DiscordBot.Application.User.Interfaces;
using DiscordBot.Domain.Common;
using DiscordBot.Domain.Models;
using DiscordBot.Domain.Monads.Result;
using DomainUser = DiscordBot.Domain.Models.Common.User;

namespace DiscordBot.Application.User;

internal sealed class DiscordUserService(
    IDiscordUserRepository discordUserRepository,
    IUserRepository userRepository) : IDiscordUserService
{
    private readonly IDiscordUserRepository _discordUserRepository =
        discordUserRepository ?? throw new ArgumentNullException(nameof(discordUserRepository));

    private readonly IUserRepository _userRepository =
        userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    /// <inheritdoc />
    public bool IsRegisteredByDiscordId(string discordId)
    {
        throw new NotImplementedException();
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
        if (isRegistered)
        {
            return Result<DiscordUser>.Error(Error.Conflict(description: "Discord id is already registered"));
        }

        var userResult = await _userRepository.RegisterByNameAsync(name);
        if (!userResult.IsSuccess)
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