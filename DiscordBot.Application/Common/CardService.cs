using DiscordBot.Application.Common.Interfaces;
using DiscordBot.Domain.Common.Enums.Card;

namespace DiscordBot.Application.Common;

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