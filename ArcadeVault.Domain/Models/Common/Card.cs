using ArcadeVault.Domain.Common.Enums.Card;

namespace ArcadeVault.Domain.Models.Common;

public class Card(Rank rank, Suit suit) : IEquatable<Card>
{
    public Rank Rank { get; private set; } = rank;
    public Suit Suit { get; private set; } = suit;

    public bool Equals(Card? other)
    {
        return other != null && Equals(this, other);
    }
}