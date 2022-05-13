import { Game } from "./game"
import { GamePlayer } from "./gamePlayer"

export interface Player {
	id: string
	publicID: string

	games: GamePlayer[]
}
