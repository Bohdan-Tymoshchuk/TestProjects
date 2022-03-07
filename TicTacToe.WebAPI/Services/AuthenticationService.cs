using System.Text.Json;
using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> Register(User user)
        {
            if (user == null || user?.Login == null || user?.Password == null)
            {
                return false;
            }

            var users = await GetAll();

            var isUserExist = users!.Any(x => x?.Login?.ToUpper() == user.Login.ToUpper());

            if (isUserExist)
            {
                return false;
            }

            await Add(user);

            return true;
        }

        public async Task<bool?> LogIn(User user)
        {
            var users = await GetAll();

            var isUserExist = users?.Any(x => x?.Login?.ToUpper() == user?.Login?.ToUpper() && x?.Password == user?.Password);

            return isUserExist;
        }

        private async Task<List<User?>?> GetAll()
        {
            var fileName = "users.json"; 
            var result = new List<User?>();
            if(File.Exists(fileName))
            {
                using var openStream = File.OpenRead(fileName);
                result = await JsonSerializer.DeserializeAsync<List<User?>>(openStream);
            }
            return result;
        }

        private async Task Add(User user)
        {
            var users = await GetAll();
            users?.Add(user);
            string jsonString = JsonSerializer.Serialize(users);
            using var writer = File.CreateText("users.json");
            await writer.WriteAsync(jsonString);
        }
    }
}
