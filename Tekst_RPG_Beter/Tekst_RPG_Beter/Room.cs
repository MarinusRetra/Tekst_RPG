using System;
using System.ComponentModel;

public class Room
{
	public string roomDescription { get; set; } // description voor de player 
	public bool Encounter { get; set; } // dit checkt of een kamer een encounter heeft
	public Entity Enemy { get; set; }

    static Random random = new Random();

	//public  List<string> roomDescriptionsEncounter = new List<string>() {"Yo", "Hoi", "Description"};
	public  List<string> roomDescriptionsSafe = new List<string>() { "SafeRoom1", "SafeRoom2", "SafeRoom3" };
 

	public Room(bool _Encounter, string _roomDescription)
	{ 
	  roomDescription = _roomDescription;
      Encounter = _Encounter;
    }
    public static Room StartRoom()
    {
        return new Room(true, "!");
    }
	public List<Room> RandomRooms(List<Entity> Zone, int _enemyChance = 70)
	{ // maak een lijst aan rooms die de speler in kan stellen om eigen moeilijkheidsgraad aan te kunnen passes

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
				kamer.roomDescription = "!";//roomDescriptionsEncounter[random.Next(0, roomDescriptionsEncounter.Count)];
            }
            else
			{ 
			    kamer.Encounter = false;
				kamer.roomDescription = roomDescriptionsSafe[random.Next(0, roomDescriptionsSafe.Count)];
            }
			RoomsList.Add(kamer);
        }
		return RoomsList;
	}
}
