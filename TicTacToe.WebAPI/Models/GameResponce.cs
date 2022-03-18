namespace TicTacToe.WebAPI.Models
{
    public class GameResponce
    {
        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }

        public Color? PlayerColor { get; set; }

        public bool IsAllowedMove { get; set; }

        public Guid? WinnerId { get; set; }

        public List<GameMove?>? GameMoves { get; set; }
    }
}
