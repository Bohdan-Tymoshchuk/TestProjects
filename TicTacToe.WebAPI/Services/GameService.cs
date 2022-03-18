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
                Player1Color = Color.Red,
                GameMoves = new()
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

        public async Task<GameResponce?> GetInfo(Guid playerId, Guid gameId)
        {
            var game = await gameRepository.GetById(gameId);

            if (game == null || game.Player1Id != playerId && game.Player2Id != playerId)
            {
                return null;
            }

            return new GameResponce
            {
                GameId = gameId,
                PlayerId = playerId,
                PlayerColor = game.Player1Id == playerId ? game.Player1Color : game.Player2Color,
                GameMoves = game.GameMoves,
                IsAllowedMove = IsAllowedMove(game, playerId),
                WinnerId = game.WinnerId
            };
        }

        private bool IsAllowedMove(Game game, Guid playerId)
        {
            return game.Player2Id != null && game.LastPlayerMoveId != playerId;
        }

        public async Task<GameMoveResponse?> SetMove(GameRequest gameRequest)
        {
            var game = await gameRepository.GetById(gameRequest.GameId);
            var gameMoveResponce = new GameMoveResponse
            {
                GameMoves = game.GameMoves,
                IsSuccessfullMove = false,
                IsWinner = false
            };
            if (game == null)
                return gameMoveResponce;

            game.LastPlayerMoveId = gameRequest.GameMove.PlayerId;


            var lastGameMove = game?.GameMoves?.FirstOrDefault(x => x.Section == gameRequest.GameMove.Section);

            if (lastGameMove == null)
            {
                game?.GameMoves?.Add(gameRequest.GameMove);
                game.WinnerId = IsWinner(gameRequest.GameMove.PlayerId, game.GameMoves) ? gameRequest.GameMove.PlayerId : null;
                gameMoveResponce.GameMoves = game.GameMoves;
                gameMoveResponce.IsSuccessfullMove = true;
                gameMoveResponce.IsWinner = IsWinner(gameRequest.GameMove.PlayerId, game.GameMoves);
                gameRepository?.Update(game);
                
                return gameMoveResponce;
            }

            if (lastGameMove.PlayerId == gameRequest.GameMove.PlayerId || lastGameMove.Number >= gameRequest.GameMove.Number)
                return gameMoveResponce;

            var gameMoveIndex = game!.GameMoves!.FindIndex(x => x.Section == lastGameMove.Section);

            game!.GameMoves[gameMoveIndex] = gameRequest.GameMove;

            game.WinnerId = IsWinner(gameRequest.GameMove.PlayerId, game.GameMoves) ? gameRequest.GameMove.PlayerId : null;

            gameMoveResponce.GameMoves = game.GameMoves;
            gameMoveResponce.IsSuccessfullMove = true;
            gameMoveResponce.IsWinner = IsWinner(gameRequest.GameMove.PlayerId, game.GameMoves);

            gameRepository?.Update(game);

            return gameMoveResponce;
        }

        private bool IsWinner(Guid playerId, List<GameMove> gameMoves)
        {
            var winCombinations = new List<List<int>>
            {
                new List<int> { 1, 2, 3},
                new List<int> { 4, 5, 6},
                new List<int> { 7, 8, 9},

                new List<int> { 1, 4, 7},
                new List<int> { 2, 5, 8},
                new List<int> { 3, 6, 9},

                new List<int> { 1, 5, 9},
                new List<int> { 3, 5, 7}
            };

            if (gameMoves is null)
            {
                return false;
            }

            var playerMovesSections = gameMoves.Where(x => x.PlayerId == playerId).Select(x => x.Section).ToList();

            foreach (var winConbination in winCombinations)
            {
                if (winConbination.All(x => playerMovesSections.Contains(x)))
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}
