using DiscordBot.Application.Common.Interfaces;
using DiscordBot.Application.Constants;
using DiscordBot.Application.User.Interfaces;
using DiscordBot.Domain.Common;
using DiscordBot.Domain.Monads.Result;

namespace DiscordBot.Application.Common;

internal sealed class TokenService(IDiscordUserRepository discordUserRepository, IUserRepository userRepository)
    : ITokenService
{
    private readonly IDiscordUserRepository _discordUserRepository =
        discordUserRepository ?? throw new ArgumentNullException(nameof(discordUserRepository));

    private readonly IUserRepository _userRepository =
        userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    /// <inheritdoc/>
    public async Task<Result<int>> ClaimDaily(string discordId)
    {
        // TODO: room for improvement
        return await _discordUserRepository.GetByDiscordId(discordId)
            .BindAsync(async user =>
            {
                var now = DateTime.Now;
                var today = DateOnly.FromDateTime(now);

                if (user.ClaimLastDaily.HasValue && user.ClaimLastDaily == today)
                {
                    var nextClaimTime = user.ClaimLastDaily.Value.ToDateTime(TimeOnly.MinValue).AddDays(1);
                    var timeUntilNextClaim = nextClaimTime - now;
                    return Result<int>.Error(Error.Validation(
                        description:
                        $"Daily reward already claimed today. You can claim again in {timeUntilNextClaim.Hours} hours and {timeUntilNextClaim.Minutes} minutes."));
                }

                var balanceUpdateResult =
                    await _userRepository.UpdateBalanceAsync(user.InternalId, GlobalConstants.DailyAmount);

                if (balanceUpdateResult.IsError)
                {
                    return Result<int>.Error(balanceUpdateResult.FirstError);
                }

                var updateUserResult = await _userRepository.UpdateAsync(user);
                return updateUserResult.IsError
                    ? Result<int>.Error(updateUserResult.FirstError)
                    : Result<int>.Success(updateUserResult.Value.Tokens);
            });
    }
}