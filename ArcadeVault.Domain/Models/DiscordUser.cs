using ArcadeVault.Domain.Models.Common;

namespace ArcadeVault.Domain.Models;

public class DiscordUser : User
{
    public string DiscordId { get; set; }
}