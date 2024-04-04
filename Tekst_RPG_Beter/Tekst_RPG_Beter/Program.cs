namespace Tekst_RPG_Beter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));
            Room.GoThroughPrisonRooms();
            Room.GoThroughForestRooms();
        }

    }
}
