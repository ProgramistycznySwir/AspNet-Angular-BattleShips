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
    public ActionResult<TileData> AddMove([FromBody] AddMoveParams @params)
    {
        var tile = _gameService.AddMove(@params.gameID, @params.playerID, @params.x, @params.y);
        if(tile is null)
            return base.BadRequest($"gameID: {@params.gameID}, playerID: {@params.playerID}, pos: (${@params.x}, ${@params.y})");
        return base.Ok(tile.GetSanitised());
    }
    //TODO: To jest tak wbrew filozofi HTTP...
    public record AddMoveParams(Guid gameID, Guid playerID, int x, int y);
    [HttpPost("CreateGame")]
    // public ActionResult<Game> CreateGame([FromBody] Guid player1_ID, [FromBody] Guid? player2_ID = null)
    public ActionResult<Game> CreateGame([FromBody] CreateGameParams @params)
    {
        Game game = _gameService.AddGame(@params.player1_ID, @params.player2_ID);

        if(game is null)
            return base.BadRequest($"player1_ID: {@params.player1_ID}, player2_ID: {@params.player2_ID}");
        return base.Ok(game.GetSanitised());
    }
    public record CreateGameParams(Guid player1_ID, Guid? player2_ID);
}
