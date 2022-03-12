namespace TicTacToe.WebAPI.Models
{
    public class Player
    {
        public Color color { get; }
        public List<int> numbers = new();
        public Player()
        {
            color = Color.Red;
            numbers.AddRange(Enumerable.Range(1, 9));
        }
        public Player(Color color)
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
