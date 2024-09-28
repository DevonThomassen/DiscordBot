using ArcadeVault.Application.User.Interfaces;
using ArcadeVault.Domain.Common;
using ArcadeVault.Domain.Models.Common;
using ArcadeVault.Domain.Monads.ErrorOr;
using ArcadeVault.Infrastructure.Common;
using ArcadeVault.Infrastructure.Database;
using ArcadeVault.Infrastructure.Database.Entities.Discord;
using ArcadeVault.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using DomainDiscordUser = ArcadeVault.Domain.Models.DiscordUser;

namespace ArcadeVault.Infrastructure.Repositories;

internal sealed class DiscordUserRepository(DatabaseContext ctx) : IDiscordUserRepository
{
    private readonly DatabaseContext _ctx = ctx ?? throw new ArgumentNullException();

    public ErrorOr<bool> IsRegistered(string discordId)
    {
        try
        {
            var entity = _ctx.DiscordUsers.FirstOrDefault(p => p.DiscordId == discordId);
            return ErrorOr<bool>.Success(entity is not null);
        }
        catch (Exception ex)
        {
            return ErrorOr<bool>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    public ErrorOr<DomainDiscordUser> GetByDiscordId(string discordId)
    {
        var entity = _ctx.DiscordUsers
            .Include(discordUserEntity => discordUserEntity.User)
            .FirstOrDefault(x => x.DiscordId == discordId);

        if (entity is null) return ErrorOr<DomainDiscordUser>.Error(Error.NotFound());
        if (entity.User is null) return ErrorOr<DomainDiscordUser>.Error(Error.NotFound());

        return ErrorOr<DomainDiscordUser>.Success(entity.ToDomain());
    }

    public ErrorOr<int> GetUserIdByDiscordId(string discordId)
    {
        try
        {
            var entity = _ctx.DiscordUsers.FirstOrDefault(x => x.DiscordId == discordId);

            return entity is null
                ? ErrorOr<int>.Error(Error.NotFound())
                : ErrorOr<int>.Success(entity.UserId);
        }
        catch (Exception ex)
        {
            return ErrorOr<int>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    public ErrorOr<bool> HasUserEnoughBalance(string discordId, int tokens)
    {
        var userResult = GetByDiscordId(discordId);

        if (!userResult.IsSuccess)
        {
            return ErrorOr<bool>.Error(Error.NotFound());
        }

        var balance = userResult.Value.Tokens;
        return tokens > balance
            ? ErrorOr<bool>.Error(Error.Validation())
            : ErrorOr<bool>.Success(true);
    }

    public async Task<ErrorOr<DomainDiscordUser>> AddDiscordToUserAsync(User user,
        string discordId)
    {
        return await SqlExecuteHelper.ExecuteAsync(async () =>
        {
            var entity = new DiscordUserEntity
            {
                DiscordId = discordId,
                UserId = user.InternalId
            };

            await _ctx.DiscordUsers.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity.ToDomain();
        });
    }
}