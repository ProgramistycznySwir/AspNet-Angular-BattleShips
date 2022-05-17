import { TileData } from "./tileData";

export interface TileDataUpdate {
	tiles: TileData[]
	turn: number
	lastMove: Date
}
