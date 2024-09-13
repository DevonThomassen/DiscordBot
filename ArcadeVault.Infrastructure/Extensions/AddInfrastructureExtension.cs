using ArcadeVault.Application.User.Interfaces;
using ArcadeVault.Infrastructure.Database;
using ArcadeVault.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArcadeVault.Infrastructure.Extensions;

public static class AddInfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        return serviceCollection
            .AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<IDiscordUserRepository, DiscordUserRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}