using ArcadeVault.Domain.Models.Common;

namespace ArcadeVault.Domain.Common.Factories;

public class DeckFactory
{
    public static Deck CreateStandardDeck(int amount = 1)
    {
        return new Deck(new Random(), amount);
    }

    public static Deck CreateShuffledDeck(int amount = 1)
    {
        var deck = new Deck(new Random(), amount);
        deck.Shuffle();
        return deck;
    }
}