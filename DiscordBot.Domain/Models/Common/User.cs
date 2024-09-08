namespace DiscordBot.Domain.Models.Common;

public class User
{
    public int InternalId { get; set; }
    public string PublicId { get; set; }
    public string Name { get; set; }
    public int Tokens { get; set; }
    public DateOnly? ClaimLastDaily { get; set; }
}