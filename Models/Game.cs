using System.ComponentModel.DataAnnotations;

namespace HappyTeam_BattleShips.Models;


public class Game
{
    [Key]
    public Guid ID { get; set; }
    public DateTime CreationTime { get; set; }
    /// <summary>
    /// To be used to delete old games.
    /// </summary>
    public DateTime LastMove { get; set; }

    public int Turn { get; set; }
    public Player GetCurrentlyMovingPlayer()
        => Players.First(e => e.SubID == Turn).Player;
    public Game IncrementPlayerTurn()
    {
        Turn++;
        if(Turn >= Players.Count)
            Turn = 0;
        return this; // Fluent
    }
    public ICollection<GamePlayer> Players { get; set; }

    public bool IsFinished => Result is not GameResult.NotFinished;
    public enum GameResult { NotFinished, Player1_Won, Player2_Won, Draw }
    public GameResult Result { get; set; }

    public static readonly (int X, int Y) BoardSize = (10, 10);
    public ICollection<TileData> BoardData { get; set; }
    public TileData GetTile(int x, int y)
        => BoardData.FirstOrDefault(tile => tile.X == x && tile.Y == y);

    public static ICollection<TileData> GenerateBoard((int X, int Y) boardSize)
    {
        var shipSizes = TileData.ShipSizes.Select(item => item).ToList();
        shipSizes.Sort();
        shipSizes.Reverse();

        TileData[,] board = new TileData[10, 10];
        Random rng = new Random();
        (int x, int y) GetRandomPos() => (rng.Next(10), rng.Next(10));

        //TODO: Implement GenerateBoard() in more tidy way. Gosh it's ugly...
        while (true)
        {
            foreach(byte size in shipSizes)
            {
                for (int @try = 16; @try > 0; @try--) // Try 16 times to place each ship before giving up
                {
                    
                }
                goto RETRY_BOARD;
                NEXT_SHIP: ;
            }
            RETRY_BOARD: ;
        }
    }
}

public static class Game_Ext 
{
    public static Game GetSanitised(this Game self)
    {
        return new Game {
            ID= self.ID,
            CreationTime= self.CreationTime,
            LastMove= self.LastMove,
            Turn= self.Turn,
            Players= self.Players.Select(e => e.GetSanitised()).ToList(),
            BoardData= self.BoardData.Select(e => e.GetSanitised()).ToList()
        };
    }
}