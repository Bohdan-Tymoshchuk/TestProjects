using TicTacToe.WebAPI.Models;
using TicTacToe.WebAPI.Repositories;

namespace TicTacToe.WebAPI.Services
{
    public class GameService : IGameService
    {
        private readonly GameRepository gameRepository;

        public GameService(GameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }
        public async Task<Game> CreateGame(Guid playerId)
        {
            var game = new Game
            {
                Player1Id = playerId,
                Player1Color = Color.Red
            };

            return await gameRepository.CreateGame(game);
        }

        public async Task<Game?> ConnectToGame(Guid playerId, Guid gameId)
        {
            var game = await gameRepository.GetById(gameId);
            if (game is null)
                return null;

            game.Player2Id = playerId;
            game.Player2Color = Color.Green;

            await gameRepository.Update(game);

            return game;
        }
    }
}
