namespace ArcadeVault.Infrastructure.Database.Entities.Common;

internal sealed class RoleEntity
{
    public int Id { get; set; }
    public required string RoleName { get; set; }
}