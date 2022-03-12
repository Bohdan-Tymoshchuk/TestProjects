namespace TicTacToe.WebAPI.Models
{
    public class Check
    {
        public int number { get; set; }
        public Color color { get; set; }
        public Check()
        {
            number = -1;
            color = Color.White;
        }
        public Check(int number, Color color)
        {
            this.number = number;
            this.color = color;
        }
    }
}
