using HappyTeam_BattleShips.Models;

namespace HappyTeam_BattleShips.Services.Interfaces
{
    public interface IPlayerService
    {
        public Player GetPlayer(PlayerID id);
        // Should strip player of base ID.
        public Player GetPlayerByPublicID(PlayerID publicID);
        public Player AddPlayer(Player player);
    }
}