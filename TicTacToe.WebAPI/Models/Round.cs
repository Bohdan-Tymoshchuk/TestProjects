namespace TicTacToe.WebAPI.Models
{
    public class Round
    {
        private static readonly List<List<int>> winningOptions = new List<List<int>> {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 5 },
            new List<int> { 6, 7, 8 },

            new List<int> { 0, 3, 6 },
            new List<int> { 1, 4, 7 },
            new List<int> { 2, 5, 8 },

            new List<int> { 0, 4, 8 },
            new List<int> { 2, 4, 6 },
        };

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public List<Check> Checks { get; set; } = new List<Check>(9);
        public Round(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            for (var i = 0; i < 9; i++)
                Checks.Add(new Check());
        }
        public List<Check> GetState()
        {
            return Checks;
        }
        public void MakeMove(Player player, int check, int number)
        {
            //you can't make a move with a number you don't already have
            //you can't make a move to a check with your color
            //you can't place number less than present
            if (!player.numbers.Contains(number) || 
                    Checks[check].color == player.color ||
                    number < Checks[check].number ||
                    number > 8 || number < 0)
                throw new ArgumentOutOfRangeException(nameof(check));

            Checks[check].number = number;
            Checks[check].color = player.color;
            player.DeleteNumber(number);
        }
        public RoundState CheckWin(Player lastPlayer, Player anotherPlayer)
        {
            var lastPlayerChecks = Checks
                .Where(x => x.color == lastPlayer.color)
                .Select(x => x.number)
                .ToList();
            
            var anotherPlayerChecks = Checks
                .Where(x => x.color == anotherPlayer.color)
                .Select(x => x.number)
                .ToList();
            
            var nonLastPlayerChecks = Checks
                .Select(x => x.number)
                .Except(lastPlayerChecks)
                .ToList();

            foreach (var winOption in winningOptions)
            {
                if (Enumerable.SequenceEqual(lastPlayerChecks.OrderBy(t => t), winOption.OrderBy(t => t)))
                    if(lastPlayer.color == PlayerOne.color)
                        return RoundState.FirstWin;
                    if (lastPlayer.color == PlayerTwo.color)
                        return RoundState.SecondWin;

                //if all checks are not empty
                //if the second (not last) player doesn't have an option to make a move
                //then it's a tie
                if (lastPlayerChecks.Count() + nonLastPlayerChecks.Count() == Checks.Count() &&
                    !CanMakeMove(anotherPlayer.numbers, lastPlayerChecks))
                    return RoundState.Tie;
            }
            return RoundState.InProgress;
        }
        public bool CanMakeMove(List<int> availableNumbers, List<int> enemyChecks)
        {
            foreach (var check in enemyChecks)
                foreach (var number in availableNumbers)
                    if (number > check)
                        return true;
            return false;
        }
    }
}
