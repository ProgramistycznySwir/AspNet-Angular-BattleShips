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
}