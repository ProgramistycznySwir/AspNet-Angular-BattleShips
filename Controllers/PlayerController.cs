using HappyTeam_BattleShips.Models;
using HappyTeam_BattleShips.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HappyTeam_BattleShips.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly IPlayerService _playerService;

    public PlayerController(ILogger<PlayerController> logger, IPlayerService playerService)
    {
        _logger = logger;
        _playerService = playerService;
    }

    [HttpGet]
    public ActionResult<Player> GetPlayer([FromRoute] PlayerID id)
    {
        var player = _playerService.GetPlayer(id: id);
        if(player is null)
            return base.NotFound(id);
        return base.Ok(player);
    }
    [HttpGet("/OtherPlayer")]
    public ActionResult<Player> GetOtherPlayer([FromRoute] PlayerID publicID)
    {
        var player = _playerService.GetPlayerByPublicID(publicID: publicID);
        if(player is null)
            return base.NotFound(publicID);
        return base.Ok(player);
    }
    [HttpPost]
    public ActionResult<Player> PostNewPlayer()
    {
        var player = _playerService.AddPlayer();
        return base.Ok(player);
    }
}
