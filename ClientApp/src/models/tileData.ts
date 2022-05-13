export interface TileData {
	x: number
	y: number
	isMiss: boolean
	isHit: boolean
	player_SubID: number
	shipSize: number
}

export enum  TileState {
	Ship_P1,
	Ship_P2,
	ShipHit_P1,
	ShipHit_P2,
	Miss_P1,
	Miss_P2
}
