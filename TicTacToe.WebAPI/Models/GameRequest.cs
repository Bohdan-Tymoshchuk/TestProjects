namespace TicTacToe.WebAPI.Models
{
    public class GameRequest
    {
        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }

        public GameMove GameMoves { get; set; }
    }
}
