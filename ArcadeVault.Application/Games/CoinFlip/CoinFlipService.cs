﻿using ArcadeVault.Application.Games.CoinFlip.Interfaces;
using ArcadeVault.Application.Games.CoinFlip.Models;
using ArcadeVault.Application.User.Interfaces;
using ArcadeVault.Domain.Common.Enums;
using ArcadeVault.Domain.Monads.ErrorOr;
using DomainCoinFlipOutcome = ArcadeVault.Domain.Games.Coinflip.CoinFlipOutcome;

namespace ArcadeVault.Application.Games.CoinFlip;

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

    public async Task<ErrorOr<CoinFlipWagerResult>> PerformWageredCoinFlipAsync(
        CoinFlipWagerRequest wagerRequest)
    {
        var balanceResult = _discordUserRepository.HasUserEnoughBalance(wagerRequest.DiscordId, wagerRequest.Amount);
        if (balanceResult.IsError)
        {
            return ErrorOr<CoinFlipWagerResult>.Error(balanceResult.FirstError);
        }

        var coin = FlipCoin();
        var gameResult = wagerRequest.WagerOutcome == coin
            ? GameResult.Win
            : GameResult.Lost;

        var discordUser = _discordUserRepository.GetByDiscordId(wagerRequest.DiscordId);

        if (discordUser.IsError)
        {
            return ErrorOr<CoinFlipWagerResult>.Error(discordUser.FirstError);
        }

        var amount = wagerRequest.WagerOutcome == coin
            ? wagerRequest.Amount
            : wagerRequest.Amount * -1;

        var updateResult = await _userRepository.UpdateBalanceAsync(discordUser.Value.InternalId, amount);

        return updateResult.IsError
            ? ErrorOr<CoinFlipWagerResult>.Error(updateResult.FirstError)
            : ErrorOr<CoinFlipWagerResult>.Success(new CoinFlipWagerResult
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