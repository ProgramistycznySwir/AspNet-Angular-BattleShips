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
    public const int NumberOfPlayers = 2;
    public ICollection<GamePlayer> Players { get; set; }

    public bool IsFinished => Result is not GameResult.NotFinished;
    public enum GameResult { NotFinished, Player1_Won, Player2_Won, Draw }
    public GameResult Result { get; set; }

    public static readonly (int X, int Y) BoardSize = (10, 10);
    public ICollection<TileData> BoardData { get; set; }
    public TileData GetTile(int x, int y)
        => BoardData.FirstOrDefault(tile => tile.X == x && tile.Y == y);

    /// <param name="boardSize">If left as null, algorithm will use default size.</param>
    public static List<TileData> GenerateBoard((int X, int Y)? boardSize= null)
    {
        (int X, int Y) boardSize_ = boardSize is null ? Game.BoardSize : boardSize.Value;
        var shipSizes = TileData.ShipSizes.Select(item => item).ToList();
        shipSizes.Sort();
        shipSizes.Reverse();

        TileData[,] board = new TileData[boardSize_.X, boardSize_.Y];
        Random rng = new Random();
        (int x, int y) GetRandomPos() => (rng.Next(boardSize_.X), rng.Next(boardSize_.Y));
        (int x, int y) GetRandomDirr() => rng.Next(4) switch { 0=>(-1,0),1=>(1,0),2=>(0,-1),3=>(0,1),_=>(-1,0)};
        bool IsInbound((int x, int y) pos) => pos.x >= 0 && pos.y >= 0 && pos.x < boardSize_.X && pos.y < boardSize_.Y;
        bool CheckPosition((int x, int y) pos, (int x, int y)? ignoreDirr= null)
        {
            if(IsInbound(pos) is false)
                return false;
            for(int x = -1; x < 2; x++)
                for(int y = -1; y < 2; y++)
                {
                    int posX = pos.x + x;
                    int posY = pos.y + y;
                    bool a = IsInbound((posX, posY));
					bool b = (x, y) != ignoreDirr;
                    bool c = false;
                    if(a && b)
					    c = board[posX, posY] is not null;
                    
                    if(a && b && c)
                        return false;
                }
                    // if(IsInbound((pos.x + x, pos.x + y)) && (x, y) != ignoreDirr && board[pos.x + x, pos.x + y] is not null)
                    //     return false;
            return true;
        }

        //TODO: Implement GenerateBoard() in more tidy way. Gosh it's ugly...
        while (true)
        {
            board = new TileData[boardSize_.X, boardSize_.Y];
            foreach(byte shipSize in shipSizes)
            {
                for(int playerSubID= 0; playerSubID < 2; playerSubID++)
                {
                    for (int @try = 16; @try > 0; @try--) // Try 16 times to place each ship before giving up
                    {
                        var(x, y) = GetRandomPos();
                        var dirr = GetRandomDirr();
                        for(int currentSize = 0; currentSize < shipSize; currentSize++)
                        {
                            var ignoreDirr_ = (-dirr.x, -dirr.y);
                            if(CheckPosition((x, y), currentSize is 0 ? null : ignoreDirr_) is false)
                            {
                                //backtrack
                                while(currentSize > 0)
                                {
                                    x -= dirr.x;
                                    y -= dirr.y;
                                    board[x, y] = null!;
                                    currentSize--;
                                }
                                goto TRY_AGAIN;
                            }
                            board[x, y] = new TileData {
                                X= (byte)x,
                                Y= (byte)y,
                                Player_SubID= playerSubID,
                                ShipSize= shipSize
                            };
                            x += dirr.x;
                            y += dirr.y;
                            //AddedShipSizeToTileData
                        }
                        goto NEXT_SHIP;
                        TRY_AGAIN: ;
                    }
                    goto RETRY_BOARD;
                    NEXT_SHIP: ;
                }
            }
            break;
            RETRY_BOARD: ;
        }

        for(int y = 0; y < 10; y++)
        {
            for(int x = 0; x < 10; x++)
                Console.Write(board[x,y] is null ? "_" : board[x, y].Player_SubID);
            Console.WriteLine();
        }


        List<TileData> result = new();
        foreach(var tile in board)
            if(tile is not null)
                result.Add(tile);
        return result;
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