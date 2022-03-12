using Newtonsoft.Json;
using System.Text;
using TicTacToeClient;

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

    ActionHelper.MainMenu();
    Console.WriteLine("Choose action:");
    ch = Console.ReadLine();


    switch(ch)
    {
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
            var httpClient = new HttpClient();

            var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/games/connect/{user?.Id}/{gameId}", null);
            var response = await responseMessage.Content.ReadAsStringAsync();
            game = JsonConvert.DeserializeObject<Game>(response);
            break;

    }

}

async Task<Game?> CreateGame(User? user)
{
    var httpClient = new HttpClient();

    var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/games/create/{user?.Id}", null);
    var response = await responseMessage.Content.ReadAsStringAsync();

    var game = JsonConvert.DeserializeObject<Game>(response);

    return game;
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

/*void ActionToSingIn()
{
    Console.WriteLine("1. Log in");
    Console.WriteLine("2. Register");
}

void Line()
{
    Console.WriteLine("___________________________________");
}

void Lock()
{
    Line();
    Console.WriteLine("You are locked for 30 seconds!");
    for (var i = 30; i > 0; i--)
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{i} secong left");
    }
    Line();
}

User EnterLoginPassword()
{
    var user = new User();
    var isCorrectPassword = true;

    Console.WriteLine("Enter login:");
    user.Login = Console.ReadLine();

    Console.WriteLine("Enter password:");
    while (isCorrectPassword)
    {
        var password = Console.ReadLine();
        if (password is null || password is "" || password.Length < 6
            || password.All(x => x == password[0]) || password is "123456")
        {
            Console.WriteLine("Bad password!");
            continue;
        }
        isCorrectPassword = false;
        user.Password = password;
    }
    return user;
}
async Task<bool> RegisterAsync()
{
    var user = EnterLoginPassword();
    var json = JsonConvert.SerializeObject(user);

    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
    var httpClient = new HttpClient();

    var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/authentifications/register", stringContent);
    var response = await responseMessage.Content.ReadAsStringAsync();
    var issSuccessfulRegistration = JsonConvert.DeserializeObject<bool>(response);

    return issSuccessfulRegistration;
}

async Task<bool> LogInAsync()
{
    var user = EnterLoginPassword();
    var json = JsonConvert.SerializeObject(user);

    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
    var httpClient = new HttpClient();

    var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/authentifications/login", stringContent);
    var response = await responseMessage.Content.ReadAsStringAsync();
    var issSuccessfulLogIn = JsonConvert.DeserializeObject<bool>(response);

    return issSuccessfulLogIn;
}*/



