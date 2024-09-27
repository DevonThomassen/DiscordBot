using ArcadeVault.Application.Games.CoinFlip.Models;
using ArcadeVault.Domain.Games.Coinflip;
using ArcadeVault.Domain.Monads.Result;

namespace ArcadeVault.Application.Games.CoinFlip.Interfaces;

public interface ICoinFlipMultiplayerService
{
    Task<Result<CoinFlipLobby>> CreateLobbyAsync(CoinFlipLobbyRequest request);
}