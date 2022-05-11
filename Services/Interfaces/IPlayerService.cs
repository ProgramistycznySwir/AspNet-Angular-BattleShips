using HappyTeam_BattleShips.Models;

namespace HappyTeam_BattleShips.Services.Interfaces;

public interface IPlayerService
{
    public Player GetPlayer(Guid id);
    // Should strip player of base ID.
    public Player GetPlayerByPublicID(Guid publicID);
    // public Player AddPlayer(Player player);
    // Overload that creates completely random player (potentially in the future players could set variables like eg. name)
    public Player AddPlayer();
}