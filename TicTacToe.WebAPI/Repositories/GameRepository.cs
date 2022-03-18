using System.Text.Json;
using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private List<Game?> games;
        private static object obj = new object();
        public async Task<Game> CreateGame(Game game)
        {
            game.Id = Guid.NewGuid();
            // TODO: Write to file
            var games = await GetAll();
            games?.Add(game);
            string jsonString = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            var fileName = "games.json";

            /*          if (File.Exists(fileName))
                      {
                          using var writer = File.CreateText(fileName);
                          await writer.WriteAsync(jsonString);
                          return game;
                      }*/

            //await File.WriteAllTextAsync(fileName, jsonString);
            lock (obj)
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.Write(jsonString);
                }
            }

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

            //var updatedGames = games?.Where(x => x?.Id == updatedGame.Id).Select(x => { x = updatedGame; return x; }).ToList();
            var index = games!.FindIndex(x => x?.Id == updatedGame.Id);
            games[index] = updatedGame;
            var jsonString = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });

            var fileName = "games.json";
            /*  if (File.Exists(fileName))
              {
                  using var writer = File.CreateText(fileName);
                  await writer.WriteAsync(jsonString);
                  return;
              }*/

            //await File.WriteAllTextAsync(fileName, jsonString);
            lock (obj)
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.Write(jsonString);
                }
            }

            // using StreamWriter outputFile = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "games.json"));
            //await outputFile.WriteAsync(jsonString);

            //await writer.WriteAsync(jsonString);
        }

        private async Task<List<Game?>?> GetAll()
        {
            /*            if (games != null)
                        {
                            return games;
                        }*/
            
            var fileName = "games.json";
            var games = new List<Game?>();
            if (File.Exists(fileName))
            {
                lock(obj)
                {
                    using var openStream = File.OpenRead(fileName);
                    games = JsonSerializer.Deserialize<List<Game?>>(openStream);
                }
            }
            return games;
        }

        


    }
}

