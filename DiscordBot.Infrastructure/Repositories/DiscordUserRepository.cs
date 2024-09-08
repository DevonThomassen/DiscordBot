using DiscordBot.Application.User.Interfaces;
using DiscordBot.Domain.Common;
using DiscordBot.Domain.Models;
using DiscordBot.Domain.Models.Common;
using DiscordBot.Domain.Monads.Result;
using DiscordBot.Infrastructure.Common;
using DiscordBot.Infrastructure.Database;
using DiscordBot.Infrastructure.Entities.Discord;
using DiscordBot.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.Infrastructure.Repositories;

internal sealed class DiscordUserRepository(DatabaseContext ctx) : IDiscordUserRepository
{
    private readonly DatabaseContext _ctx = ctx ?? throw new ArgumentNullException();

    public bool IsRegistered(string discordId)
    {
        var entity = _ctx.DiscordUsers.FirstOrDefault(p => p.DiscordId == discordId);
        return entity is not null;
    }

    public Result<DiscordUser> GetByDiscordId(string discordId)
    {
        var entity = _ctx.DiscordUsers
            .Include(discordUserEntity => discordUserEntity.User)
            .FirstOrDefault(x => x.DiscordId == discordId);

        if (entity is null) return Result<DiscordUser>.Error(Error.NotFound());
        if (entity.User is null) return Result<DiscordUser>.Error(Error.NotFound());

        return Result<DiscordUser>.Success(entity.ToDomain());
    }

    public Result<int> GetUserIdByDiscordId(string discordId)
    {
        var entity = _ctx.DiscordUsers.FirstOrDefault(x => x.DiscordId == discordId);

        return entity is null
            ? Result<int>.Error(Error.NotFound())
            : Result<int>.Success(entity.UserId);
    }

    public Result<bool> HasUserEnoughBalance(string discordId, int tokens)
    {
        var userResult = GetByDiscordId(discordId);

        if (!userResult.IsSuccess)
        {
            return Result<bool>.Error(Error.NotFound());
        }

        var balance = userResult.Value.Tokens;
        return tokens > balance
            ? Result<bool>.Error(Error.Validation())
            : Result<bool>.Success(true);
    }

    public async Task<Result<DiscordUser>> AddDiscordToUserAsync(User user, string discordId)
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