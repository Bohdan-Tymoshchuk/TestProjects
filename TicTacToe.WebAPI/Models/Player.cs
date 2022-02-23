namespace TicTacToe.WebAPI.Models
{
    public class Player
    {
        public Colors color { get; }
        public List<int> numbers = new();
        public Player()
        {
            color = Colors.Red;
            numbers.AddRange(Enumerable.Range(1, 9));
        }
        public Player(Colors color)
        {
            this.color = color;
            numbers.AddRange(Enumerable.Range(1, 9));
        }
        public void DeleteNumber(int number)
        {
            numbers.Remove(number);
        }
    }
}
