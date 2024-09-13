using System.ComponentModel.DataAnnotations;
using ArcadeVault.Infrastructure.Entities.Common;

namespace ArcadeVault.Infrastructure.Entities.Discord;

internal sealed class DiscordUserEntity
{
    [Key] public required string DiscordId { get; set; }
    public required int UserId { get; set; }

    public UserEntity? User { get; set; }
}