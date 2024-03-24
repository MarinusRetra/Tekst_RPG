namespace Tekst_RPG_Beter
{
    internal class Program
    {


        static void Main(string[] args)
        {
            // de laatste parameter is nodig om te kiezen van welke script hij de functies pakt
            PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));
        }
    }
}