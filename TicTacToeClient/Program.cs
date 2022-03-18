using Newtonsoft.Json;
using System.Text;

using TicTacToeClient;
using TicTacToeClient.Models;

const string _baseApiUrl = "http://localhost:5094";
var ch = string.Empty;
while (ch != "0")
{
    //var user = await Authorization();
    var user = new User
    {
        Id = Guid.Parse("29dce3c8-39e8-4b03-883f-a1af976adb29"),
        Login = "b",
        Password = "123321"
    };
    var game = new Game();
    var gameResponce = new GameResponce();
    var gameMove = new GameMove();
    var gameMoveResponce = new GameMoveResponse();
    var botId = Guid.NewGuid();
    ActionHelper.MainMenu();
    Console.WriteLine("Choose action:");
    ch = Console.ReadLine();


    switch(ch)
    {
        case "1":
            game = await CreateGame(user);
            game = await ConnectToGame(botId, game!.Id);
            while (!gameMoveResponce.IsWinner)
            {
                gameResponce = await GetInfo(user.Id, game!.Id);

                if (gameResponce.WinnerId == botId)
                {
                    PrintGameArea(gameResponce.GameMoves);
                    Console.WriteLine("Game over!");
                    break;
                }
                if (!gameResponce!.IsAllowedMove)
                {
                    var httpClient = new HttpClient();
                    var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/games/bot/{botId}/{game.Id}", null);
                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var isSuccess = JsonConvert.DeserializeObject<bool?>(response);
                    continue;
                }
                PrintGameArea(gameResponce.GameMoves);
                gameMove = EnterGameMove(user.Id, gameResponce.PlayerColor);

                var gameRequest = new GameRequest()
                {
                    GameId = game.Id,
                    GameMove = gameMove
                };

                gameMoveResponce = await SendRequest(gameRequest);
                if (gameMoveResponce!.IsSuccessfullMove)
                {
                    PrintGameArea(gameMoveResponce?.GameMoves);
                }

                if(gameMoveResponce.IsWinner)
                    Console.WriteLine("You are winner!");
            }
            break;
        case "2":
            game = await CreateGame(user);

            break;
        case "3":
            Console.WriteLine("Enter id game:");
            var isGuid = Guid.TryParse(Console.ReadLine(), out var gameId);
            if(!isGuid)
            {
                Console.WriteLine("Wrong id");
            }
            game = await ConnectToGame(user.Id, gameId);
            
            break;

    }

}

GameMove EnterGameMove(Guid playerId, Color? color)
{
    var gameMove = new GameMove();
    var isNumber = false;
    while (!isNumber)
    {
        Console.WriteLine("Enter section:");
        isNumber = int.TryParse(Console.ReadLine(), out var section);
        if (!isNumber || section > 10 || section < 0)
        {
            ActionHelper.WrongData("section");
            continue;
        }

        Console.WriteLine("Enter number:");
        isNumber = int.TryParse(Console.ReadLine(), out var number);
        if (!isNumber || section > 10 || section < 0)
        {
            ActionHelper.WrongData("number");
            continue;
        }
        gameMove.PlayerId = playerId;
        gameMove.Section = section;
        gameMove.Number = number;
        gameMove.Color = color;
    }
    return gameMove;
}

List<MoveForPrint?>? ConvertMoves(List<GameMove?>? gameMoves)
{
    var emptyStr = Enumerable.Repeat(" ", 10).ToList();
    var movesForPrint = new List<MoveForPrint?>();
    foreach(var move in emptyStr)
    {
        movesForPrint.Add(new MoveForPrint
        {
            Color = Color.White,
            GameMove = move
        });
    }
    if (gameMoves is null)
        return movesForPrint;

    foreach(var move in gameMoves)
    {
        movesForPrint[move!.Section]!.GameMove = move.Number.ToString();
        movesForPrint[move!.Section]!.Color = move.Color;
    }
    return movesForPrint;
}

void PrintGameArea(List<GameMove?>? gameMoves)
{
    //gameMoves = gameMoves?.OrderBy(x => x.Section).ToList();
    
    var gameMovesStr = ConvertMoves(gameMoves);
    Console.WriteLine("     |     |      ");

    Console.ForegroundColor = (ConsoleColor)gameMovesStr[1].Color;
    Console.Write($"  {gameMovesStr[1]?.GameMove}  ");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");

    Console.ForegroundColor = (ConsoleColor)gameMovesStr[2].Color;
    Console.Write($"  {gameMovesStr[2]?.GameMove}  ");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");

    Console.ForegroundColor = (ConsoleColor)gameMovesStr[3].Color;
    Console.WriteLine($"  {gameMovesStr[3]?.GameMove}  ");


    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("_____|_____|_____ "); 
    Console.WriteLine("     |     |      ");


    Console.ForegroundColor = (ConsoleColor)gameMovesStr[4].Color;
    Console.Write($"  {gameMovesStr[4]?.GameMove}  ");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");

    Console.ForegroundColor = (ConsoleColor)gameMovesStr[5].Color;
    Console.Write($"  {gameMovesStr[5]?.GameMove}  ");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");

    Console.ForegroundColor = (ConsoleColor)gameMovesStr[6].Color;
    Console.WriteLine($"  {gameMovesStr[6]?.GameMove}  ");


    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("_____|_____|_____ ");
    Console.WriteLine("     |     |      ");


    Console.ForegroundColor = (ConsoleColor)gameMovesStr[7].Color;
    Console.Write($"  {gameMovesStr[7]?.GameMove}  ");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");

    Console.ForegroundColor = (ConsoleColor)gameMovesStr[8].Color;
    Console.Write($"  {gameMovesStr[8]?.GameMove}  ");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");

    Console.ForegroundColor = (ConsoleColor)gameMovesStr[9].Color;
    Console.WriteLine($"  {gameMovesStr[9]?.GameMove}  ");

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("     |     |      ");

    ActionHelper.Line();
}

async Task<Game?> CreateGame(User? user)
{
    var httpClient = new HttpClient();

    var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/games/create/{user?.Id}", null);
    var response = await responseMessage.Content.ReadAsStringAsync();

    var game = JsonConvert.DeserializeObject<Game?>(response);

    return game;
}

async Task<Game?> ConnectToGame(Guid userId, Guid gameId)
{
    var httpClient = new HttpClient();
    var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/games/connect/{userId}/{gameId}", null);
    var response = await responseMessage.Content.ReadAsStringAsync();
    var game = JsonConvert.DeserializeObject<Game>(response);
    return game;
}

async Task<GameMoveResponse?> SendRequest(GameRequest gameRequest)
{
    var json = JsonConvert.SerializeObject(gameRequest);

    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

    var httpClient = new HttpClient();

    var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/games/move", stringContent);
    var response = await responseMessage.Content.ReadAsStringAsync();

    return JsonConvert.DeserializeObject<GameMoveResponse>(response);
}

async Task<GameResponce?> GetInfo(Guid userId, Guid gameId)
{
    var httpClient = new HttpClient();
    var responseMessage = await httpClient.GetAsync($"{_baseApiUrl}/api/games/info/{userId}/{gameId}");
    var response = await responseMessage.Content.ReadAsStringAsync();
    var gameResponce = JsonConvert.DeserializeObject<GameResponce?>(response);
    return gameResponce;
}

async Task<User?> Authorization()
{
    var httpClient = new HttpClient();
    var attempt = 0;
    
    User? user = null;

    while (user is null)
    {
        ActionHelper.ActionToSignIn();
        Console.WriteLine("Choose acton:");
        ch = Console.ReadLine();
        ActionHelper.Line();
        switch (ch)
        {
            case "1":
                user = await Authentification.LogInAsync();
                if (user is null)
                {
                    Console.WriteLine("Wrong Login or Password!");
                    ActionHelper.Line();
                    attempt++;
                }
                break;
            case "2":
                user = await Authentification.RegisterAsync();
                if (user is null)
                {
                    Console.WriteLine("This Login is already exist!");
                    ActionHelper.Line();
                    attempt++;
                }
                break;
        }
        if (attempt == 3)
            Authentification.Lock();
    }

    return user;
}





