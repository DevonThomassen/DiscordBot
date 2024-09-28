using ArcadeVault.Application.Games.CoinFlip.Models;
using ArcadeVault.Domain.Games.Coinflip;
using ArcadeVault.Domain.Monads.ErrorOr;

namespace ArcadeVault.Application.Games.CoinFlip.Interfaces;

public interface ICoinFlipMultiplayerService
{
    Task<ErrorOr<CoinFlipLobby>> CreateLobbyAsync(CoinFlipLobbyRequest request);
}