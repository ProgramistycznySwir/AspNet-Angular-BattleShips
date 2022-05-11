export interface TileData {
	x: number
	y: number
	state: TileState
}

export enum  TileState {
	Ship_P1,
	Ship_P2,
	ShipHit_P1,
	ShipHit_P2,
	Miss_P1,
	Miss_P2
}
