using ArcadeVault.Application.Common.Interfaces;
using ArcadeVault.Domain.Common.Enums.Card;

namespace ArcadeVault.Application.Common;

internal sealed class CardService : ICardService
{
    /// <inheritdoc/>
    public IEnumerable<string> GetSuits()
    {
        return Enum.GetValues(typeof(Suit)).Cast<Suit>()
            .Select(x => x.ToString())
            .ToList();
    }

    /// <inheritdoc/>
    public IEnumerable<string> GetRanks()
    {
        return Enum.GetValues(typeof(Rank)).Cast<Rank>()
            .Select(x => x.ToString())
            .ToList();
    }
}