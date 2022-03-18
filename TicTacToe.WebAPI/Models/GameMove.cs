namespace TicTacToe.WebAPI.Models
{
    public class GameMove
    {
        public Guid PlayerId { get; set; }

        public Color? Color { get; set; }

        public int Section { get; set; }

        public int Number { get; set; }
    }
}
