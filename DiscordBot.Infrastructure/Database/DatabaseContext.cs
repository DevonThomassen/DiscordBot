using DiscordBot.Infrastructure.Entities.Common;
using DiscordBot.Infrastructure.Entities.Discord;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.Infrastructure.Database;

internal sealed class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<DiscordUserEntity> DiscordUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RoleEntity>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<RoleEntity>().HasData([
            new RoleEntity
            {
                Id = 1,
                RoleName = "Admin"
            },
            new RoleEntity
            {
                Id = 2,
                RoleName = "User"
            }
        ]);

        modelBuilder.Entity<UserEntity>()
            .HasKey(p => p.UserId);

        modelBuilder.Entity<UserEntity>()
            .Property(p => p.RoleId)
            .HasDefaultValue(1);

        modelBuilder.Entity<DiscordUserEntity>()
            .HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}