import { GamePlayer } from "./gamePlayer"
import { Player } from "./player"
import { TileData } from "./tileData"

export interface Game {

	id: string
	creationTime: Date
	lastMove: Date

	turn: number
	players: GamePlayer[]

	isFinished: boolean
	result: GameResult
	boardData: TileData[]
}


export enum GameResult {
	NotFinished,
	Player1_Won,
	Player2_Won,
	Draw
}
