namespace ArcadeVault.Application.Common.Interfaces;

public interface ICardService
{
    /// <summary>
    /// Retrieves all possible card suits.
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetSuits();

    /// <summary>
    /// Retrieves all possible card ranks.
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetRanks();
}