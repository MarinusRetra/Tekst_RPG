public class Room
{
    public string roomDescription { get; set; } // description van de kamer voor de player 
    public bool Encounter { get; set; } // dit checkt of een kamer een encounter heeft
    public Entity Enemy { get; set; }

    static Random random = new Random();

    List<string> roomDescriptionsSafePrison = new List<string>() { "While making your way through the prison you see a small puzzle engraved on one of the walls.", "While waiting for some guards to pass. you see a small puzzle on the floor, you decide to your hand at solving it. ", "You search one of the lockers in the guards changing area. In one of the lockers lies a small puzzle" };
    List<string> roomDescriptionsSafeForest = new List<string>() { "You run into a an old couple playing board games in their backyard. They seem friendly, you decide to join them for a game", "A magical board is engraved inside the trunk of a tree, it looks like a game.", "An old automaton used for playing board games lies dormant against a tree, You try to play it for a game" };

    public static List<string> roomDescriptionsPrisonPeople = new List<string>() { };
    public static List<string> roomDescriptionsForestPeople = new List<string>() { };

    public Room(bool _Encounter, string _roomDescription)
    {
        roomDescription = _roomDescription;
        Encounter = _Encounter;

    }
    public static void PickEvent(List<Entity> currentZone)
    {
        if (currentZone == Zones.PrisonPeople)
        {
            PrisonRoomEventStart();
        }
        if (currentZone == Zones.ForestPeople)
        {
            ForestRoomEventStart();
        }
    }
    static void PrisonRoomEventStart()
    {
        Minigames.BoterKaas("Schuif"); //een aangepaste versie van boterkaas en eieren waar je symbolen moet schuifen
    }

    static void ForestRoomEventStart()
    {
        Minigames.BoterKaas();//een boterkaas en eieren spel
    }

    public static Room StartRoom()
    {
        return new Room(true, "!");
    }
    public List<Room> RandomRooms(List<Entity> Zone, int _enemyChance = 60)
    {
        var newZone = new List<Entity>(Zone);
        List<string> roomDescriptionsEncounter = new List<string> { };
        if (Zone == Zones.PrisonPeople)
        {
            roomDescriptionsEncounter = roomDescriptionsPrisonPeople;
        }
        else if (Zone == Zones.ForestPeople)
        {
            roomDescriptionsEncounter = roomDescriptionsForestPeople;
        }

        List<Room> RoomsList = new List<Room>();

        for (int i = 0; i <= newZone.Count - 1; i++)
        {
            int RandomGetal = random.Next(1, 101);
            Room kamer = new Room(false, "");
            if (RandomGetal < _enemyChance)// als RandomGetal lager is dan 60 gebeurt dit
            {
                kamer.Encounter = true;
                kamer.Enemy = newZone.ElementAt(random.Next(0, newZone.Count - 1));//pakt een random entity uit de list. kan alles pakken behalve de laatste van de list
                kamer.roomDescription = roomDescriptionsEncounter.ElementAt(newZone.IndexOf(kamer.Enemy));// pakt de description van EncounterDescList op hetzelfde element als het gepakte element van de Zone list
                roomDescriptionsEncounter.RemoveAt(newZone.IndexOf(kamer.Enemy));
                newZone.Remove(kamer.Enemy);// zorgt dat je niet twee keer dezelfde kamer in de list kan krijgen
            }
            else
            {
                kamer.Encounter = false;
                if (Zone == Zones.PrisonPeople)
                { 
                    kamer.roomDescription = roomDescriptionsSafePrison[random.Next(0, roomDescriptionsSafePrison.Count)];
                    if (roomDescriptionsSafePrison.Count <= 1)
                    {
                       // zorgt dat je alleen twee keer dezelfde puzzel description kan krijgen als je door alle andere descriptions heen bent gegaan
                        roomDescriptionsSafePrison.Remove(kamer.roomDescription);
                    }
                }
                else
                { 
                   kamer.roomDescription = roomDescriptionsSafeForest[random.Next(0, roomDescriptionsSafeForest.Count)];
                   if (roomDescriptionsSafeForest.Count <= 1)
                   {
                        // zorgt dat je alleen twee keer dezelfde puzzel description kan krijgen als je door alle andere descriptions heen bent gegaan
                       roomDescriptionsSafeForest.Remove(kamer.roomDescription);
                   }
                }
            }
            RoomsList.Add(kamer);
        }
        return RoomsList;
    }

    public static void GoThroughPrisonRooms()
    {
        var room = Room.StartRoom();
        List<Room> InsertRooms = room.RandomRooms(Zones.PrisonPeople);

        for (int i = InsertRooms.Count-1; InsertRooms.Count >= 1; i--)
        {
            var kamer = InsertRooms[i];
            if (kamer.Encounter)
            {
                if (kamer.Enemy.Health > 0)
                {
                    Console.Clear();
                    Instelbaar.Print($"{kamer.roomDescription}");
                    Console.ReadLine();
                    Combat.StartCombat(kamer.Enemy);
                }
                else
                {
                    throw new Exception("Je hebt meerderkeren dezelfde enemy gekregen in je kamer");
                }
            }
            else
            {
                Console.Clear();
                Instelbaar.Print(kamer.roomDescription);
                Console.ReadLine();
                Room.PickEvent(Zones.PrisonPeople);

            }
            InsertRooms.Remove(kamer);
        }
        Console.Clear();
        Instelbaar.Print("You hear heavy footsteps further down the hall");
        Console.ReadLine();
        Console.Clear();
        Instelbaar.Print(Room.roomDescriptionsPrisonPeople.Last());
        Console.ReadLine();
        Combat.StartCombat(Zones.PrisonPeople.Last());
        Instelbaar.Print("As you set foot outside outside the prison you find yourself in a lush forest.");
        Console.ReadLine();
    }


    public static void GoThroughForestRooms()
    {
        var room = Room.StartRoom();
        List<Room> InsertRooms = room.RandomRooms(Zones.ForestPeople);

        for (int i = InsertRooms.Count - 1; InsertRooms.Count >= 1; i--)
        {
            var kamer = InsertRooms[i];
            if (kamer.Encounter)
            {
                if (kamer.Enemy.Health > 0)
                {
                    Console.Clear();
                    Instelbaar.Print($"{kamer.roomDescription}");
                    Console.ReadLine();
                    Combat.StartCombat(kamer.Enemy);
                }
                else
                {
                    throw new Exception("Je hebt meerderkeren dezelfde enemy gekregen in je kamer");
                }
            }
            else
            {
                Console.Clear();
                Instelbaar.Print(kamer.roomDescription);
                Console.ReadLine();
                Room.PickEvent(Zones.ForestPeople);
            }
            InsertRooms.Remove(kamer);
        }
        Console.Clear();
        Instelbaar.Print("An elf warrior approaches");
        Console.ReadLine();
        Console.Clear();
        Instelbaar.Print(Room.roomDescriptionsForestPeople.Last());
        Console.ReadLine();
        Combat.StartCombat(Zones.ForestPeople.Last());
    }
}
