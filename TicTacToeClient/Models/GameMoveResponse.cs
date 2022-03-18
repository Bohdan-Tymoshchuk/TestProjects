namespace TicTacToeClient.Models
{
    public class GameMoveResponse
    {
        public bool IsSuccessfullMove { get; set; }

        public bool IsWinner { get; set; }

        public List<GameMove?>? GameMoves { get; set; }
    }
}
