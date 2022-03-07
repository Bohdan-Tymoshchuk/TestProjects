using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Services
{
    public interface IAuthenticationService
    {
        public Task<bool> Register(User user);
        public Task<bool?> LogIn(User user);
    }
}
