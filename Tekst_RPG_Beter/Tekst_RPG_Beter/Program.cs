namespace Tekst_RPG_Beter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));

            //var room = Room.StartRoom();
            //List<Room> StarRooms = room.RandomRooms(100,70,10);
            //
            //foreach (Room kamer in StarRooms)
            //{
            //    Console.WriteLine($"{kamer.roomDescription}\nIs encounter: {kamer.Encounter}");
            //    if (kamer.Encounter)
            //    {
            //        // combat
            //    }
            //    else 
            //    {
            //        // niet combat
            //    }
            //}
        }
    }
}