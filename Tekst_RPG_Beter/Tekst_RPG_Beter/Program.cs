namespace Tekst_RPG_Beter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));

            var room = Room.StartRoom();
            List<Room> PrisonRooms = room.RandomRooms(Zones.PrisonPeople, 70);
            
            foreach (Room kamer in PrisonRooms)
            {
                Instelbaar.Print($"{kamer.roomDescription}");
                Console.ReadLine();
                if (kamer.Encounter)
                {
                    if (kamer.Enemy.Health > 0)
                    { 
                     Combat.StartCombat(kamer.Enemy);
                    }
                    else
                    Instelbaar.Print("De persoon die je tegenkomt is al dood");
                }
                else 
                {
                     Instelbaar.Print(kamer.roomDescription);
                     Console.ReadLine();// selector (optie1, optie2, optie3)
                }
            }
        }
    }
}