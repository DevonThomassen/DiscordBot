namespace DiscordBot.Domain.Models.Common;

public class Bet
{
    public User User { get; private set; }
    public decimal Amount { get; private set; }

    public Bet(User user, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Bet amount must be positive.");
        }

        User = user;
        Amount = amount;
    }
}