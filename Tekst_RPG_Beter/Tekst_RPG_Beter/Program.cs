namespace Tekst_RPG_Beter
{
    internal class Program
    {
        static void Main(string[] args)
        {
          PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));
          
          var room = Room.StartRoom();
          List<Room> InsertRooms = room.RandomRooms(Zones.PrisonPeople, 70);
          
          foreach (Room kamer in InsertRooms)
          {
              if (kamer.Encounter)
              {
                    if (kamer.Enemy.Health > 0)
                    {
                        Console.Clear();
                        Instelbaar.Print(kamer.roomDescription);
                        Console.ReadLine();
                        Combat.StartCombat(kamer.Enemy);
                    }
                    else
                        continue;
              }
              else
              {
                  Console.Clear();
                  Instelbaar.Print(kamer.roomDescription);
                  Console.ReadLine();
                  Room.PickEvent(Zones.PrisonPeople);
              }    
          }
          Console.Clear();
          Instelbaar.Print("You hear large footsteps further down the hall");
          Console.ReadLine();
          Console.Clear();
          Instelbaar.Print(Room.roomDescriptionsPrisonPeople.Last());
          Console.ReadLine();
          Combat.StartCombat(InsertRooms.Last().Enemy);
        }
    }
}