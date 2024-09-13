using System.ComponentModel.DataAnnotations;

namespace ArcadeVault.Infrastructure.Entities.Common;

internal class UserEntity
{
    [Key] public int UserId { get; set; }
    public required string PublicId { get; set; }
    public required string Name { get; set; }
    public int Tokens { get; set; }
    public DateOnly? ClaimLastDaily { get; set; }
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; }
}