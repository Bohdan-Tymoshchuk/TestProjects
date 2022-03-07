using Newtonsoft.Json;
using System.Text;
using TicTacToeClient;

const string _baseApiUrl = "http://localhost:5094";
var httpClient = new HttpClient();
var attempt = 0;
var ch = string.Empty;
while(ch != "0")
{
    ActionToSingIn();
    Console.WriteLine("Choose acton:");
    ch = Console.ReadLine();
    Line();
    var isSuccessful = false;
    switch (ch)
    {
        case "1":
            isSuccessful = await LogInAsync();
            if (!isSuccessful)
            {
                Console.WriteLine("Wrong Login or Password!");
                Line();
                attempt++;
            }  
            break;
        case "2":
            isSuccessful = await RegisterAsync();
            if (!isSuccessful)
            {
                Console.WriteLine("This Login is already exist!");
                Line();
                attempt++;
            }
            break;
    }
    if (attempt == 3)
        Lock();
}

void ActionToSingIn()
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
}



