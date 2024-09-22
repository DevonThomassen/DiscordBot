using ArcadeVault.Application.User.Interfaces;
using ArcadeVault.Domain.Common;
using ArcadeVault.Domain.Models.Common;
using ArcadeVault.Domain.Monads.Result;
using ArcadeVault.Infrastructure.Common;
using ArcadeVault.Infrastructure.Database;
using ArcadeVault.Infrastructure.Database.Entities.Common;
using ArcadeVault.Infrastructure.Extensions;

namespace ArcadeVault.Infrastructure.Repositories;

internal sealed class UserRepository(DatabaseContext ctx) : IUserRepository
{
    private readonly DatabaseContext _ctx = ctx ?? throw new ArgumentNullException();

    /// <inheritdoc />
    public async Task<Result<User>> RegisterByNameAsync(string name)
    {
        return await SqlExecuteHelper.ExecuteAsync(async () =>
        {
            var entity = new UserEntity
            {
                PublicId = Ulid.NewUlid().ToString(),
                Name = name,
                Tokens = Constants.StartMoney
            };

            await _ctx.Users.AddAsync(entity);
            await _ctx.SaveChangesAsync();

            return entity.ToDomain();
        });
    }

    /// <inheritdoc />
    public Result<User> GetByIdAsync(int internalId)
    {
        try
        {
            var entity = _ctx.Users.FirstOrDefault(x => x.UserId == internalId);

            if (entity is null)
            {
                return Result<User>.Error(Error.NotFound());
            }

            return Result<User>.Success(new User()
            {
                Name = entity.Name,
                PublicId = entity.PublicId
            });
        }
        catch (Exception ex)
        {
            return Result<User>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    /// <inheritdoc />
    public Result<User> GetByPublicId(string publicId)
    {
        try
        {
            var entity = _ctx.Users.FirstOrDefault(x => x.PublicId == publicId);

            return entity is null
                ? Result<User>.Error(Error.NotFound())
                : Result<User>.Success(entity.ToDomain());
        }
        catch (Exception ex)
        {
            return Result<User>.Error(Error.Unexpected(description: ex.Message));
        }
    }

    /// <inheritdoc />
    public async Task<Result<User>> UpdateAsync(User user)
    {
        var entity = await _ctx.Users.FindAsync(user.InternalId);

        if (entity is null)
        {
            return Result<User>.Error(Error.NotFound());
        }

        entity.ClaimLastDaily = DateOnly.FromDateTime(DateTime.UtcNow);
        await _ctx.SaveChangesAsync();

        return Result<User>.Success(entity.ToDomain());
    }

    /// <inheritdoc />
    public async Task<Result<int>> UpdateBalanceAsync(int publicId, int amount)
    {
        var entity = await _ctx.Users.FindAsync(publicId);

        if (entity is null)
        {
            return Result<int>.Error(Error.NotFound());
        }

        entity.Tokens += amount;
        await _ctx.SaveChangesAsync();

        return Result<int>.Success(entity.Tokens);
    }

    /// <inheritdoc />
    public async Task<Result<User>> UpdateNameAsync(int publicId, string name)
    {
        var entity = await _ctx.Users.FindAsync(publicId);

        if (entity is null)
        {
            return Result<User>.Error(Error.NotFound());
        }

        entity.Name = name;
        await _ctx.SaveChangesAsync();

        return Result<User>.Success(entity.ToDomain());
    }

    /// <inheritdoc />
    public Task<bool> DeleteUserAsync()
    {
        throw new NotImplementedException();
    }
}