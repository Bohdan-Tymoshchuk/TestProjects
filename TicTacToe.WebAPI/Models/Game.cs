namespace TicTacToe.WebAPI.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        public Guid Player1Id { get; set; }

        public Guid? Player2Id { get; set; }

        public Guid? WinnerId { get; set; }

        public Color Player1Color { get; set; }

        public Color? Player2Color { get; set; }

        public List<GameMove>? GameMoves { get; set; }
    }
}
