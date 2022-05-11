using HappyTeam_BattleShips.Models;

namespace HappyTeam_BattleShips.Services.Interfaces
{
    public interface IGameService
    {
        public Game GetGame(GameID id);
        public Game AddGame(PlayerID id1, PlayerID id2);
        public TileData AddMove(GameID id, TileData tileData);
    }
}