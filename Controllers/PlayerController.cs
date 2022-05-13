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

    [HttpGet("{id}")]
    public ActionResult<Player> GetPlayer([FromRoute] Guid id)
    {
        var player = _playerService.GetPlayer(id: id);
        if(player is null)
            return base.NotFound(id);
        return base.Ok(player.GetSanitised_RemainID());
    }
    [HttpGet("/OtherPlayer/{publicID}")]
    public ActionResult<Player> GetOtherPlayer([FromRoute] Guid publicID)
    {
        var player = _playerService.GetPlayerByPublicID(publicID: publicID);
        if(player is null)
            return base.NotFound(publicID);
        return base.Ok(player.GetSanitised());
    }
    //TODO: Currently games are fetched through this controller, but to be RESTful games should be fetched with different query from GameController.
    [HttpPost]
    public ActionResult<Player> PostNewPlayer()
    {
        var player = _playerService.AddPlayer();
        return base.Ok(player);
    }
}
