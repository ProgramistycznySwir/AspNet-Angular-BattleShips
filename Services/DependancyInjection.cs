using HappyTeam_BattleShips.Controllers;
using HappyTeam_BattleShips.Services.Interfaces;

namespace HappyTeam_BattleShips.Services;

public static class DependancyInjection
{
    public static void RegisterDependancies(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPlayerService, PlayerService>();
        builder.Services.AddScoped<IGameService, GameService>();
        // builder.Services.AddScoped<ICommunicationService, >();
        builder.Services.AddSingleton<WebSocketHub>();
    }
}