using HappyTeam_BattleShips.Models;

namespace HappyTeam_BattleShips.Services.Interfaces;

// Deals with communicating data to users.
public interface ICommunicationService
{
    public void NotifyOtherPlayerOfBoardUpdate(Guid playerID_toNotify, TileData about);
}