using ArcadeVault.Domain.Monads.Result;

namespace ArcadeVault.Application.Common.Interfaces;

public interface ILobbyService
{
    Task CreateLobbyAsync();
    Task<Result<T>> FinalizeLobbyAsync<T>(string lobbyId);
}