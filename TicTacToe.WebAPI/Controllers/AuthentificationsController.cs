using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.WebAPI.Models;
using TicTacToe.WebAPI.Services;

namespace TicTacToe.WebAPI.Controllers
{
    [Route("api/authentifications")]
    [ApiController]
    public class AuthentificationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthentificationsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<User?> Login([FromBody] User user)
        {
            return await _authenticationService.LogIn(user);
        }

        [HttpPost("register")]
        public async Task<User?> Register([FromBody] User user)
        {
            return await _authenticationService.Register(user);
        }
    }
}
