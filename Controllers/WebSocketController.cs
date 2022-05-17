// using System.Net.WebSockets;
using HappyTeam_BattleShips.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HappyTeam_BattleShips.Controllers;

public class WebSocketHub : Hub
{
	static readonly Dictionary<Guid, string> ConnectedUsers = new();

	public async Task RegisterPlayer(Guid playerID)
	{
		if(ConnectedUsers.TryGetValue(playerID, out string _))
			ConnectedUsers.Remove(playerID);
		ConnectedUsers.Add(playerID, this.Context.ConnectionId);
	}
	public async Task DisconnectPlayer(Guid playerID)
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
		public const string RegisterPlayer = "registerPlayer";
		public const string DisconnectPlayer = "disconnectPlayer";
	}
}