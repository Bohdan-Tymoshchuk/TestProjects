using System.Text.Json;
using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Repositories
{
    public class GameRepository : IGameRepository
    { 
        public async Task<Game> CreateGame(Game game)
        {
            game.Id = Guid.NewGuid();
            // TODO: Write to file
            var games = await GetAll();
            games?.Add(game);
            string jsonString = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            using var writer = File.CreateText("games.json");
            await writer.WriteAsync(jsonString);
             
            return game;
        }

        public async Task<Game?> GetById(Guid gameId)
        {
            var games = await GetAll();
            var game = games?.FirstOrDefault(x => x?.Id == gameId);
            return game;
        }

        public async Task Update(Game updatedGame)
        {
            var games = await GetAll();

            var updatedGames = games?.Where(x => x?.Id == updatedGame.Id).Select(x => { x = updatedGame; return x; }).ToList();
            string jsonString = JsonSerializer.Serialize(updatedGames, new JsonSerializerOptions { WriteIndented = true });
            using var writer = File.CreateText("games.json");
            await writer.WriteAsync(jsonString);
        }

        private async Task<List<Game?>?> GetAll()
        {
            var fileName = "games.json";
            var result = new List<Game?>();
            if (File.Exists(fileName))
            {
                using var openStream = File.OpenRead(fileName);
                result = await JsonSerializer.DeserializeAsync<List<Game?>>(openStream);
            }
            return result;
        }


    }
}

