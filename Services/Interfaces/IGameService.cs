using HappyTeam_BattleShips.Models;

namespace HappyTeam_BattleShips.Services.Interfaces;

public interface IGameService
{
    /// <summary>
    /// Caution! Use this only when game ends, as this reveals position of other player's ships.
    /// </summary>
    public Game GetGame(GameID id);
    public Game GetGameFromPerspective(GameID id, PlayerID perspective_ID);
    public Game AddGame(PlayerID id1);
    public Game AddGame(PlayerID id1, PlayerID id2);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameID"></param>
    /// <param name="playerID">Pass Player.ID, not PublicID</param>
    /// <returns>Result (hit or miss on enemy ship)</returns>
    public TileData AddMove(GameID gameID, PlayerID playerID, int x, int y);
}