using DiscordBot.Application.User.Interfaces;
using DiscordBot.Domain.Monads.Result;
using DomainUser = DiscordBot.Domain.Models.Common.User;

namespace DiscordBot.Application.User;

internal class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository =
        userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    /// <inheritdoc/>
    public async Task<Result<DomainUser>> RegisterAsync(string name)
    {
        return await _userRepository.RegisterByNameAsync(name);
    }

    /// <inheritdoc/>
    public Result<DomainUser> GetByPublicId(string userId)
    {
        return _userRepository.GetByPublicId(userId);
    }
}