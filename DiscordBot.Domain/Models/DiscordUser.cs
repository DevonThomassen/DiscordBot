using DiscordBot.Domain.Models.Common;

namespace DiscordBot.Domain.Models;

public class DiscordUser : User
{
    public string DiscordId { get; set; }
}