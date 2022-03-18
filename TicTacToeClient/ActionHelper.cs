namespace TicTacToeClient
{
    public static class ActionHelper
    {
        public static void ActionToSignIn()
        {
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Register");
        }

        public static void WrongData(string name)
        {
            Console.WriteLine($"Wrong {name}!");
            Console.WriteLine("Try enter again!");
            Line();
        }

        public static void Line()
        {
            Console.WriteLine("___________________________________");
        }

        public static void MainMenu()
        {
            Console.WriteLine("1. Game with bot");
            Console.WriteLine("2. Create new private game");
            Console.WriteLine("3. Connect to private game");
            Console.WriteLine("4. Public game");
        }
    }
}
