using DiscordBot.Domain.Common.Enums.Card;

namespace DiscordBot.Domain.Models.Common;

public class Deck
{
    private readonly List<Card> _cards;
    private readonly Random _random;

    public int CardAmount => _cards.Count;
    public int DeckAmount { get; }

    public Deck(Random random, int deckAmount = 1)
    {
        _cards = InitCards();
        _random = random;
        DeckAmount = deckAmount;
    }

    public virtual void Shuffle()
    {
        for (var i = _cards.Count - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    public void Remove(Card card)
    {
        _cards.Remove(card);
    }

    private static List<Card> InitCards()
    {
        return Enum.GetValues(typeof(Suit)).Cast<Suit>()
            .SelectMany(suit => Enum.GetValues(typeof(Rank)).Cast<Rank>()
                .Select(rank => new Card(rank, suit))).ToList();
    }
}