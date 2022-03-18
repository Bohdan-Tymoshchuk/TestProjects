using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.WebAPI.Models;
using TicTacToe.WebAPI.Services;

namespace TicTacToe.WebAPI.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IBotService? _botService;


        public GameController(IGameService gameService, IBotService? botService)
        {
            _gameService = gameService;
            _botService = botService;
        }

        [HttpPost("create/{playerId}")]
        public async Task<Game> Create(Guid playerId)
        {
            return await _gameService.CreateGame(playerId);
        }

        [HttpPost("connect/{playerId}/{gameId}")]
        public async Task<Game?> Connect(Guid playerId, Guid gameId)
        {
            return await _gameService.ConnectToGame(playerId, gameId);
        }

        [HttpGet("info/{playerId}/{gameId}")]
        public async Task<GameResponce?> GetInfo(Guid playerId, Guid gameId)
        {
            return await _gameService.GetInfo(playerId, gameId);
        }

        [HttpPost("move")]
        public Task<GameMoveResponse?> SetMove(GameRequest gameRequest)
        {
            return _gameService.SetMove(gameRequest);
        }

        [HttpPost("bot/{botId}/{gameId}")]
        public async Task<bool?> MakeBotMove(Guid botId, Guid gameId)
        {
            return await _botService.MakeBotMove(botId, gameId);
        }
    }
}
