using TicTacToe.WebAPI.Models;
using TicTacToe.WebAPI.Repositories;

namespace TicTacToe.WebAPI.Services
{
    public interface IGameService
    {
        public Task<Game> CreateGame(Guid playerId);
        public Task<Game?> ConnectToGame(Guid playerId, Guid gameId);
    }
}