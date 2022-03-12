using System.Text.Json;
using System.Xml;
using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<User?> Register(User user)
        {
            if (user == null || user?.Login == null || user?.Password == null)
            {
                return null;
            }

            user.Id = Guid.NewGuid();

            var users = await GetAll();

            var isUserExist = users!.Any(x => x?.Login?.ToUpper() == user.Login.ToUpper());

            if (isUserExist)
            {
                return null;
            }

            await Add(user);

            return user;
        }

        public async Task<User?> LogIn(User user)
        {
            var users = await GetAll();

            var foundUser = users?.FirstOrDefault(x => x?.Login?.ToUpper() == user?.Login?.ToUpper() && x?.Password == user?.Password);

            return foundUser;
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
            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            using var writer = File.CreateText("users.json");
            await writer.WriteAsync(jsonString);
        }
    }
}
