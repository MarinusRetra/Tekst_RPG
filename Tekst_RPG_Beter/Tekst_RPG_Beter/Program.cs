namespace Tekst_RPG_Beter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));
            Console.WriteLine($"You have beaten the game and {PlayerChoices.Player.Name} is now free! Congratulations!");
            Console.ReadLine();
        }

    }
}
