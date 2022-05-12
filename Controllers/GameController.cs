using HappyTeam_BattleShips.Models;
using HappyTeam_BattleShips.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HappyTeam_BattleShips.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameService _gameService;

    public GameController(ILogger<GameController> logger, IGameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }

    //TODO: Lock this endpoint from users, cause if used it grants unfair advantage.
    // [HttpGet("{gameID}")]
    // public ActionResult<Game> GetGame([FromRoute] Guid gameID)
    // {
    //     var game = _gameService.GetGame(id: gameID);
    //     if(game is null)
    //         return base.NotFound(gameID);
    //     return base.Ok(game.GetSanitised());
    // }
    [HttpGet("{gameID}/{perspectiveID}")]
    public ActionResult<Game> GetGameFromPerspective([FromRoute] Guid gameID, [FromRoute] Guid perspectiveID)
    {
        var game = _gameService.GetGameFromPerspective(gameID, perspectiveID);
        if(game is null)
            return base.NotFound($"gameID: {gameID}, perspectiveID: {perspectiveID}");
        return base.Ok(game.GetSanitised());
    }
    [HttpPost("AddMove")]
    public ActionResult<TileData> AddMove(Guid gameID, Guid playerID, int x, int y)
    {
        var tile = _gameService.AddMove(gameID, playerID, x, y);
        if(tile is null)
            return base.BadRequest($"gameID: {gameID}, playerID: {playerID}, pos: (${x}, ${y})");
        return base.Ok(tile.GetSanitised());
    }
    [HttpPost("CreateGame")]
    public ActionResult<Game> CreateGame(Guid player1_ID, Guid? player2_ID = null)
    {
        Game game = _gameService.AddGame(player1_ID, player2_ID);

        if(game is null)
            return base.BadRequest($"player1_ID: {player1_ID}, player2_ID: {player2_ID}");
        return base.Ok(game.GetSanitised());
    }
}
