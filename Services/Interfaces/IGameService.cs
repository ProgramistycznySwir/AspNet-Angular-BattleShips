using HappyTeam_BattleShips.Models;

namespace HappyTeam_BattleShips.Services.Interfaces;

public interface IGameService
{
    /// <summary>
    /// Caution! Use this only when game ends, as this reveals position of other player's ships.
    /// </summary>
    public Game GetGame(Guid id);
    public Game GetGameFromPerspective(Guid id, Guid perspective_ID);
    public Game AddGame(Guid publicID1, Guid? publicID2= null);
    public Game AddPlayerToGame(Guid gameID, Guid publicID2);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameID"></param>
    /// <param name="playerID">Pass Player.ID, not PublicID</param>
    /// <returns>Result (hit or miss on enemy ship)</returns>
    public TileData AddMove(Guid gameID, Guid playerID, int x, int y);
    public TileDataUpdateDTO CheckGameUpdate(Guid gameID, Guid playerID, DateTime lastUpdate);
}