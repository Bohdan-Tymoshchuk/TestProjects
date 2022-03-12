using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Services
{
    public interface IAuthenticationService
    {
        public Task<User?> Register(User user);
        public Task<User?> LogIn(User user);
    }
}
