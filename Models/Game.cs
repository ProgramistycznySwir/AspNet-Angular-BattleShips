using System.ComponentModel.DataAnnotations;
using StronglyTypedIds;

namespace HappyTeam_BattleShips.Models;


[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid,
        converters: StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct GameID{}

public class Game
{
    [Key]
    public GameID ID { get; set; }
    public DateTime CreationTime { get; set; }
    /// <summary>
    /// To be used to delete old games.
    /// </summary>
    public DateTime LastMove { get; set; }

    public ICollection<Player> Players { get; set; }

    public bool IsFinished => Result is not GameResult.NotFinished;
    public enum GameResult { NotFinished, Player1_Won, Player2_Won, Draw }
    public GameResult Result { get; set; }

    public ICollection<TileData> BoardData { get; set; }
    public TileData.TileState? GetTile(int x, int y)
        => BoardData.FirstOrDefault(tile => tile.X == x && tile.Y == y)?.State;
}