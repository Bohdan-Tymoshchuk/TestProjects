using Microsoft.AspNetCore.Mvc;
using TicTacToe.WebAPI.Models;

namespace TicTacToe.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Check> Get()
        {
            var playerOne = new Player(Colors.Red);
            var playerTwo = new Player(Colors.Green);
            var round = new Round(playerOne, playerTwo);
            
            //TODO: handling exception
            round.MakeMove(playerOne, 0, 1);
            
            return round.GetState();
        }
    }
}