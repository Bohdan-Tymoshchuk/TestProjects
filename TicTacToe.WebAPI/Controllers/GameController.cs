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

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
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
    }
}
