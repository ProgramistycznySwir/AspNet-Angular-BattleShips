import { Player } from "./player"
import { TileData } from "./tileData"

export interface GamePlayer {

	subID: number,
	game_ID: string,
	player_ID: string
}


export enum GameResult {
	NotFinished,
	Player1_Won,
	Player2_Won,
	Draw
}
