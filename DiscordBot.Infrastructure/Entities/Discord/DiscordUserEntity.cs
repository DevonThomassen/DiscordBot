using DiscordBot.Infrastructure.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace DiscordBot.Infrastructure.Entities.Discord;

internal sealed class DiscordUserEntity
{
    [Key] public required string DiscordId { get; set; }
    public required int UserId { get; set; }

    public UserEntity? User { get; set; }
}