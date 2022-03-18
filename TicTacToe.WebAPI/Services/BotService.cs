using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Services
{
    public class BotService : IBotService
    {
        private readonly IGameService _gameService;

        public BotService(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<bool?> MakeBotMove(Guid botId, Guid gameId)
        {
            var gameResponce = await _gameService.GetInfo(botId, gameId);
            var gameMoves = gameResponce?.GameMoves;
            
            var rnd = new Random();
            var number = 0;
            var section = rnd.Next(1, 10);
            var selectedSection = gameResponce?.GameMoves?.LastOrDefault(x => x?.Section == section);
            if (selectedSection is null)
                number = rnd.Next(1, 10);
            
            if (selectedSection?.Number is not null)
                number = rnd.Next(selectedSection.Number + 1, 10);

            var gameRequest = new GameRequest
            {
                GameId = gameId,
                GameMove = new GameMove
                {
                    Number = number,
                    Section = section,
                    PlayerId = botId,
                    Color = gameResponce?.PlayerColor
                }
            };
            var gameMoveResponce = await _gameService.SetMove(gameRequest);
            return gameMoveResponce.IsSuccessfullMove;


        }
    }
}
