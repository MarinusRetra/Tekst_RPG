using System.Diagnostics.Contracts;

public class Room
{
	public string roomDescription { get; set; } // description voor de player 
	public bool Encounter { get; set; } // dit checkt of een kamer een encounter heeft
	public Entity Enemy { get; set; }

	static Random random = new Random();

	//public  List<string> roomDescriptionsEncounter = new List<string>() {"Yo", "Hoi", "Description"};
	public List<string> roomDescriptionsSafePrison = new List<string>() { "While making your way through the prison you see a small puzzle engraved on one of the walls.", "While waiting for some guards to pass. you see a small puzzle on the floor, you decide to your hand at solving it. ", "You search one of the lockers in the guards changing area. In one of the lockers lies a small puzzle"};
	public List<string> roomDescriptionsSafeForest = new List<string>() { "You run into a an old couple playing board games in their backyard. They seem friendly, you decide to join them for a game", "A magical board is engraved inside the trunk of a tree, it looks like a game.", "An old automaton used for playing board games lies dormant against a tree, You try to play it for a game"};

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
	public List<Room> RandomRooms(List<Entity> Zone,  int _enemyChance = 70)
	{ // maak een lijst aan rooms die de speler in kan stellen om eigen moeilijkheidsgraad aan te kunnen passes

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

		for (int i = 0; i <= Zone.Count-1; i++) // deze loop loopt nog 1 keer ookal wordt er geen entity meer gezet aan kamer.Enemy,
		// want de laatste element wordt nooit gepakt in de eerste if statement
	    {
		    int RandomGetal = random.Next(1,101);
			Room kamer = new Room(false,"");
			if (RandomGetal < _enemyChance)// als RandomGetal lager is dan 70 gebeurt dit
			{
				kamer.Encounter = true;
			    kamer.Enemy = Zone.ElementAt(random.Next(0,Zone.Count - 1));//pakt een random entity uit de list. kan alles pakken behalve de laatste van de list
			    kamer.roomDescription = roomDescriptionsEncounter.ElementAt(Zone.IndexOf(kamer.Enemy));// pakt de description van EncounterDescList op hetzelfde element als het gepakte element van de Zone list
            }
            else
			{ 
			    kamer.Encounter = false;
				if (Zone == Zones.PrisonPeople)
				{
					kamer.roomDescription = roomDescriptionsSafePrison[random.Next(0, roomDescriptionsSafePrison.Count)];
				
				}
				else
					kamer.roomDescription = roomDescriptionsSafeForest[random.Next(4, roomDescriptionsSafeForest.Count)];

            }
            RoomsList.Add(kamer);
        }
		return RoomsList;
	}


}