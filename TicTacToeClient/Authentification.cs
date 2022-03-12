using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeClient
{
    public class Authentification
    {
        const string _baseApiUrl = "http://localhost:5094";

        public static User EnterLoginPassword()
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
                    Console.WriteLine("Incorrect password!");
                    continue;
                }
                isCorrectPassword = false;
                user.Password = password;
            }
            return user;
        }
        public static async Task<User?> RegisterAsync()
        {
            var user = EnterLoginPassword();
            var json = JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();

            var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/authentifications/register", stringContent);
            var response = await responseMessage.Content.ReadAsStringAsync();
            var issSuccessfulRegistration = JsonConvert.DeserializeObject<User?>(response);

            return issSuccessfulRegistration;
        }

        public static async Task<User?> LogInAsync()
        {
            var user = EnterLoginPassword();
            var json = JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();

            var responseMessage = await httpClient.PostAsync($"{_baseApiUrl}/api/authentifications/login", stringContent);
            var response = await responseMessage.Content.ReadAsStringAsync();
            var issSuccessfulLogIn = JsonConvert.DeserializeObject<User?>(response);

            return issSuccessfulLogIn;
        }

        public static void Lock()
        {
            ActionHelper.Line();
            Console.WriteLine("You are locked for 30 seconds!");
            for (var i = 30; i > 0; i--)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{i} secong left");
            }
            ActionHelper.Line();
        }
    }
}
