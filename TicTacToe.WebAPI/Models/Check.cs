namespace TicTacToe.WebAPI.Models
{
    public class Check
    {
        public int number { get; set; }
        public Colors color { get; set; }
        public Check()
        {
            number = -1;
            color = Colors.White;
        }
        public Check(int number, Colors color)
        {
            this.number = number;
            this.color = color;
        }
    }
}
