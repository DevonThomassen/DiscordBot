using ArcadeVault.Application.Games.CoinFlip.Interfaces;
using ArcadeVault.Application.Games.CoinFlip.Models;
using ArcadeVault.Domain.Games.Coinflip;
using ArcadeVault.Domain.Monads.ErrorOr;

namespace ArcadeVault.Application.Games.CoinFlip;

public class CoinFlipMultiplayerService : ICoinFlipMultiplayerService
{
    public Task<ErrorOr<CoinFlipLobby>> CreateLobbyAsync(CoinFlipLobbyRequest request)
    {
        throw new NotImplementedException();
    }
}