namespace DiscordBot.Infrastructure.Entities.Common;

internal sealed class RoleEntity
{
    public int Id { get; set; }
    public required string RoleName { get; set; }
}