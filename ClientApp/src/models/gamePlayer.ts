import { Player } from "./player"
import { TileData } from "./tileData"

export interface GamePlayer {

	subID: number,
	game_ID: number,
	player_ID: number

	/*
    public class GamePlayer
    {
        [Key]
        public int SubID { get; set; }
        [Key]
        public Guid Game_ID { get; set; }
        public Game Game { get; set; }


        public Guid Player_ID { get; set; }
        public Player Player { get; set; }
    }
	 */
}


export enum GameResult {
	NotFinished,
	Player1_Won,
	Player2_Won,
	Draw
}
