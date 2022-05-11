using HappyTeam_BattleShips.Models;

namespace HappyTeam_BattleShips.Services.Interfaces
{
    public interface IPlayerService
    {
        public Player GetPlayer(PlayerID id);
        public Player AddPlayer(Player player);
    }
}