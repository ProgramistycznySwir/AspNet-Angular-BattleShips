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

    public Game AddGame(Guid publicID1, Guid? publicID2= null)
    {
        var newGame_ID = new Guid();
        var newGame = new Game {
                ID = newGame_ID,
                CreationTime = DateTime.Now,
                LastMove = DateTime.Now,
            };
        if(newGame is null)
            return null;
        var player1 = _context.Players.Where(e => e.PublicID == publicID1).FirstOrDefault();
        if(player1 is null)
            return null;
        newGame.Players = new List<GamePlayer> {
                new GamePlayer { 
                        SubID = 0,
                        Game_ID = newGame.ID,
                        Player_ID = player1.ID,
                    }
            };
        if(publicID2 is not null)
        {
            var player2 = _context.Players.Where(e => e.PublicID == publicID2).FirstOrDefault();
            if(player2 is null)
                return null;
            newGame.Players.Add(new GamePlayer { 
                    SubID = 1,
                    Game_ID = newGame.ID,
                    Player_ID = player2.ID,
                });
        }
        List<TileData> board = Game.GenerateBoard();
        for(int i = 0; i < board.Count; i++)
            board[i].Game_ID = newGame.ID;
        newGame.BoardData = board;

        _context.Games.Add(newGame);
        _context.SaveChanges();
        return newGame;
    }


    //TODO: Implement propper error messages.
    /// <returns>Null indicates invalid request.</returns>
    public TileData AddMove(Guid gameID, Guid playerID, int x, int y)
    {
        var game = GetGame(gameID);
        if(game is null)
            return null;

        var gamePlayer = game.Players.FirstOrDefault(player => player.Player_ID == playerID);
        if(gamePlayer is null)
            return null;

        var tile = game.GetTile(x, y);

        if(tile is null)
            game.BoardData.Add(tile = new TileData { Game_ID = game.ID, X= (byte)x, Y= (byte)y, IsMiss = true, Player_SubID = gamePlayer.SubID });
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
        => _context.Games.Where(e => e.ID == id)
                .Include(e => e.BoardData)
                .Include(e => e.Players)
                .ThenInclude(e => e.Player)
                .FirstOrDefault()!;

    public Game GetGameFromPerspective(Guid id, Guid perspective_ID)
    {
        var game = _context.Games.Where(e => e.ID == id)
                .Include(e => e.BoardData)
                .Include(e => e.Players)
                .ThenInclude(e => e.Player)
                .AsNoTracking()
                .FirstOrDefault();
        if(game is null)
            return null;
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