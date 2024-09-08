using DiscordBot.Application.Games.CoinFlip.Models;
using DiscordBot.Application.User.Interfaces;
using DiscordBot.Domain.Common.Enums;
using DiscordBot.Domain.Monads.Result;
using DomainCoinFlipOutcome = DiscordBot.Domain.Games.Coinflip.CoinFlipOutcome;

namespace DiscordBot.Application.Games.CoinFlip;

internal sealed class CoinFlipService(
    IDiscordUserRepository discordUserRepository,
    IUserRepository userRepository) : ICoinFlipService
{
    private readonly IDiscordUserRepository _discordUserRepository =
        discordUserRepository ?? throw new ArgumentNullException(nameof(discordUserRepository));

    private readonly IUserRepository _userRepository =
        userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    private static readonly Random Random = new();

    public DomainCoinFlipOutcome Flip()
    {
        return FlipCoin();
    }

    public SimulateCoinFlipResult SimulateCoinFlip(CoinFlipSimulationRequest simulationRequest)
    {
        var coin = FlipCoin();
        var gameResult = coin.Equals(coin)
            ? GameResult.Win
            : GameResult.Lost;

        return new SimulateCoinFlipResult
        {
            GameResult = gameResult,
            CoinFlipOutcome = coin,
        };
    }

    public async Task<Result<CoinFlipWagerResult>> PerformWageredCoinFlipAsync(CoinFlipWagerRequest wagerRequest)
    {
        var balanceResult = _discordUserRepository.HasUserEnoughBalance(wagerRequest.DiscordId, wagerRequest.Amount);
        if (balanceResult.IsError)
        {
            return Result<CoinFlipWagerResult>.Error(balanceResult.FirstError);
        }

        var coin = FlipCoin();
        var gameResult = wagerRequest.WagerOutcome == coin
            ? GameResult.Win
            : GameResult.Lost;

        var discordUser = _discordUserRepository.GetByDiscordId(wagerRequest.DiscordId);

        if (discordUser.IsError)
        {
            return Result<CoinFlipWagerResult>.Error(discordUser.FirstError);
        }

        var amount = wagerRequest.WagerOutcome == coin
            ? wagerRequest.Amount
            : wagerRequest.Amount * -1;

        var updateResult = await _userRepository.UpdateBalanceAsync(discordUser.Value.InternalId, amount);

        return updateResult.IsError
            ? Result<CoinFlipWagerResult>.Error(updateResult.FirstError)
            : Result<CoinFlipWagerResult>.Success(new CoinFlipWagerResult
            {
                GameResult = gameResult,
                CoinFlipOutcome = coin,
                NewBalance = amount
            });
    }

    private static DomainCoinFlipOutcome FlipCoin()
    {
        return Random.Next(2) == 0
            ? DomainCoinFlipOutcome.Heads
            : DomainCoinFlipOutcome.Tails;
    }
}