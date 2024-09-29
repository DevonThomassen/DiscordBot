using ArcadeVault.Application.User.Interfaces;
using ArcadeVault.Domain.Monads.ErrorOr;
using DomainUser = ArcadeVault.Domain.Models.Common.User;

namespace ArcadeVault.Application.User;

internal class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository =
        userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    /// <inheritdoc/>
    public async Task<ErrorOr<DomainUser>> RegisterAsync(string name)
    {
        return await _userRepository.RegisterByNameAsync(name);
    }

    /// <inheritdoc/>
    public ErrorOr<DomainUser> GetByPublicId(string userId)
    {
        return _userRepository.GetByPublicId(userId);
    }
}