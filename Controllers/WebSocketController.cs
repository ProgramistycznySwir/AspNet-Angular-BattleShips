// using System.Net.WebSockets;
using HappyTeam_BattleShips.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HappyTeam_BattleShips.Controllers;

public class WebSocketController : Hub
{
	static readonly Dictionary<Guid, string> ConnectedUsers = new();

	public async Task Register(Guid playerID)
	{
		ConnectedUsers.Add(playerID, this.Context.ConnectionId);
	}
	public async Task Disconnect(Guid playerID)
	{
		ConnectedUsers.Remove(playerID);
	}

	public async Task UpdateTileData(Guid playerID, TileData tileData)
	{
		if(ConnectedUsers.TryGetValue(playerID, out string value))
			await Clients.User(value).SendAsync(WebSocketActions.UpdateTileData, tileData);
	}

	public struct WebSocketActions
	{
        public const string UpdateTileData = "updateTileData";
	}
}