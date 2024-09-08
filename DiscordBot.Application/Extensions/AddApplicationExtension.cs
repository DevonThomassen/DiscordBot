using DiscordBot.Application.Common;
using DiscordBot.Application.Common.Interfaces;
using DiscordBot.Application.Games.CoinFlip;
using DiscordBot.Application.User;
using DiscordBot.Application.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Application.Extensions;

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