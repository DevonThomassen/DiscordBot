using ArcadeVault.Application.Common;
using ArcadeVault.Application.Common.Interfaces;
using ArcadeVault.Application.Games.CoinFlip;
using ArcadeVault.Application.Games.CoinFlip.Interfaces;
using ArcadeVault.Application.User;
using ArcadeVault.Application.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ArcadeVault.Application;

public static class AddApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ICoinFlipService, CoinFlipService>()
            .AddScoped<IDiscordUserService, DiscordUserService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICardService, CardService>();
    }
}