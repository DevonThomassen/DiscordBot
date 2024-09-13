namespace ArcadeVault.Domain.Models.Common;

public class Hand()
{
    public List<Card> Cards { get; set; }
    public User User { get; set; }
}