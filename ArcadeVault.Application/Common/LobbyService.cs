using ArcadeVault.Application.Common.Interfaces;
using ArcadeVault.Domain.Monads.ErrorOr;

namespace ArcadeVault.Application.Common;

internal sealed class LobbyService : ILobbyService
{
    public Task CreateLobbyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<T>> FinalizeLobbyAsync<T>(string lobbyId)
    {
        throw new NotImplementedException();
    }
}