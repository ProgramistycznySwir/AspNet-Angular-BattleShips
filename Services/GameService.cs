using HappyTeam_BattleShips.Data;
using HappyTeam_BattleShips.Models;
using HappyTeam_BattleShips.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyTeam_BattleShips.Services;

public class GameService : IGameService
{
	private readonly AppDbContext _context;

    public GameService(AppDbContext context)
    {
        _context = context;
    }

    public Game AddGame(Guid id1)
    {
        var newGame_ID = new Guid();
        var newGame = new Game {
                ID = newGame_ID,
                CreationTime = DateTime.Now,
                LastMove = DateTime.Now,
            };
        _context.Games.Add(newGame);
        newGame.Players = new List<GamePlayer> { 
            new GamePlayer { 
                    SubID = 0,
                    Game_ID = newGame_ID,
                    Player_ID = id1
                }};
        _context.SaveChanges();
        return newGame;
    }

    public Game AddGame(Guid id1, Guid id2)
    {
        var newGame = AddGame(id1);
        newGame.Players.Add(new GamePlayer { 
                SubID = 0,
                Game_ID = newGame.ID,
                Player_ID = id2
            });
        _context.SaveChanges();
        return newGame;
    }


    //TODO: Implement propper error messages.
    /// <returns>Null indicates invalid request.</returns>
    public TileData AddMove(Guid gameID, Guid playerID, int x, int y)
    {
        // _context.Games.Include(e => e.BoardData).Include(e => e.Players).Find(gameID)
        var game = GetGame(gameID);
        if(game is null)
            return null;

        var gamePlayer = game.Players.FirstOrDefault(player => player.Player_ID == playerID);
        if(gamePlayer is null)
            return null;

        var tile = game.GetTile(x, y);

        if(tile is null)
            game.BoardData.Add(new TileData { Game_ID = game.ID, X= (byte)x, Y= (byte)y, IsMiss = true, Player_SubID = gamePlayer.SubID });
        else if(tile.IsMiss)
            return null;
        else if(tile.Player_SubID == gamePlayer.SubID)
            return null;
        else 
            tile.IsHit = true;

        //TODO: Implement notifying other players via WebSockets.
        _context.SaveChanges();

        return tile;
    }
    // private TileData AddMove(Game game, )

    public Game GetGame(Guid id)
    {
        return _context.Games.Where(e => e.ID == id)
                .Include(e => e.BoardData)
                .Include(e => e.Players)
                .FirstOrDefault();
    }

    public Game GetGameFromPerspective(Guid id, Guid perspective_ID)
    {
        var game = _context.Games.Where(e => e.ID == id)
                .Include(e => e.BoardData)
                .Include(e => e.Players)
                .AsNoTracking()
                .FirstOrDefault();
        var gamePlayer = game.Players.Where(player => player.Player_ID == perspective_ID).FirstOrDefault();
        if(gamePlayer is null) // Invalid perspective.
            return null;
        // Sanitising BoardData.
        game.BoardData = game.BoardData.Where(tile => tile.IsMiss is true
                                                    || tile.IsHit is true
                                                    || tile.Player_SubID == gamePlayer.SubID)
                .ToList();
        return game;
    }
}