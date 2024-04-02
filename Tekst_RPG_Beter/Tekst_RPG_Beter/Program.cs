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
                      Combat.StartCombat(kamer.Enemy);
                  }
                  else
                      Instelbaar.Print("De persoon die je tegenkomt is al dood");
              }
              else
              {
                  Instelbaar.Print(kamer.roomDescription);
                  Console.ReadLine();
                  Console.Clear();
                  Room.PickEvent(Zones.PrisonPeople);
              }    
          }
          Console.Clear();
          Instelbaar.Print("Je hoort een grote vijand aankomen");
          PlayerChoices.Selector("Verder","CheckInventory","WachtEnKijk", 2 , typeof(PlayerChoices));
          Combat.StartCombat(InsertRooms.Last().Enemy);
        }
    }
}