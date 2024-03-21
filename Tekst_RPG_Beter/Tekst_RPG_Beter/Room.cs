using System;

public class Room
{
	public string roomDescription; // description voor de player 
	public bool Encounter; // dit checkt of een kamer een encounter heeft
	public List<string> roomDescriptions = new List<string>() {"Yo", "Hoi", "Description"};

	public Room(bool Encounter, string roomDescription)
	{ 
	  
	}   
     

	public void StartRoom()
	{
		Room Start = new Room(true, "!");
	}
	public void RandomRooms(int _roomCount, int _enemyChance, int _lootChance)
	{ // maak een lijst aan rooms die de speler in kan stellen om eigen moeilijkheidsgraad aan te kunnen passes
	  
	  List<Room> rooms = new List<Room>();
	  Random random = new Random();

	   for (int i = 0; i <= _roomCount; i++)
       {
			rooms.Add(new Room(false, "Yo"));
       }

	   foreach (Room room in rooms) 
	   { 
		    int isEnemy = random.Next(_enemyChance, 100);
			if (isEnemy < _enemyChance)// als enemychance 70 gebeurt dit 70% van de tijd
			{
				room.Encounter = true;
				room.roomDescription = room.roomDescriptions[random.Next(0, roomDescriptions.Count)];
			}
			else
			{ 
			    room.Encounter = false;
				room.roomDescription = room.roomDescriptions[random.Next(0, roomDescriptions.Count)];
			}
	   }
	   
		 
	  
	}
		//maak een enemy class met een hasLoot bool die op dezelfde manier bekanst wordt als hier
}
