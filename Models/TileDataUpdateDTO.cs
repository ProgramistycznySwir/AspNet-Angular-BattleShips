

namespace HappyTeam_BattleShips.Models;

public class TileDataUpdateDTO
{
    public ICollection<TileData> Tiles { get; set; }
    public int Turn { get; set; }
    public DateTime LastMove { get; set;}
}