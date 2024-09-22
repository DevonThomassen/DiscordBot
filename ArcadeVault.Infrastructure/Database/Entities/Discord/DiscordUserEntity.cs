using ArcadeVault.Infrastructure.Database.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ArcadeVault.Infrastructure.Database.Entities.Discord;

internal sealed class DiscordUserEntity
{
    [Key] public required string DiscordId { get; set; }
    public required int UserId { get; set; }

    public UserEntity? User { get; set; }
}