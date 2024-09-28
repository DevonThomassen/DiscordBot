using ArcadeVault.Domain.Monads.ErrorOr;

namespace ArcadeVault.Application.Common.Interfaces;

public interface ILobbyService
{
    Task CreateLobbyAsync();
    Task<ErrorOr<T>> FinalizeLobbyAsync<T>(string lobbyId);
}