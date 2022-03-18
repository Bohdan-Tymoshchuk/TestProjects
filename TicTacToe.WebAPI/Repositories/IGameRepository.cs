using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Repositories
{
    public interface IGameRepository
    {
        public Task<Game> CreateGame(Game game);
        public Task<Game?> GetById(Guid gameId);
        public Task Update(Game updatedGame);
    }
}
