import { Game } from "./game"

export interface Player {
	id: string
	publicID: string

	games: Game[]
	/*
    [Key]
    public PlayerID ID { get; set; }
    /// <summary>
    /// Used when creating games, as ID is only visible to player, not to others.
    /// </summary>
    public PlayerID PublicID { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime LastUsedTime { get; set; }
    
    public ICollection<GamePlayer> Games { get; set; }
	 */
}
