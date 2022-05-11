using HappyTeam_BattleShips.Data;
using HappyTeam_BattleShips.Models;
using HappyTeam_BattleShips.Services.Interfaces;

namespace HappyTeam_BattleShips.Services;

public class GameService : IGameService
{
	private readonly AppDbContext _context;

    public Game AddGame(Guid id1)
    {
        throw new NotImplementedException();
    }

    public Game AddGame(Guid id1, Guid id2)
    {
        throw new NotImplementedException();
    }

    public TileData AddMove(Guid gameID, Guid playerID, int x, int y)
    {
        throw new NotImplementedException();
    }

    public Game GetGame(Guid id)
    {
        throw new NotImplementedException();
    }

    public Game GetGameFromPerspective(Guid id, Guid perspective_ID)
    {
        throw new NotImplementedException();
    }
}