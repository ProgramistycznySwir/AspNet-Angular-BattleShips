using HappyTeam_BattleShips.Data;
using HappyTeam_BattleShips.Models;
using HappyTeam_BattleShips.Services.Interfaces;

namespace HappyTeam_BattleShips.Services;

public class GameService : IGameService
{
	private readonly AppDbContext _context;

    public Game AddGame(PlayerID id1)
    {
        throw new NotImplementedException();
    }

    public Game AddGame(PlayerID id1, PlayerID id2)
    {
        throw new NotImplementedException();
    }

    public TileData AddMove(GameID gameID, PlayerID playerID, int x, int y)
    {
        throw new NotImplementedException();
    }

    public Game GetGame(GameID id)
    {
        throw new NotImplementedException();
    }

    public Game GetGameFromPerspective(GameID id, PlayerID perspective_ID)
    {
        throw new NotImplementedException();
    }
}