using System;
using System.Runtime.CompilerServices;
using Tekst_RPG_Beter;

public class Entity
{
	public bool usedSkill;

	public int maxHealth;
	public string Name { get; set; }
	public int Health { get; set; }
	public int Damage { get; set; }
	public int Level { get; set; }
	public int XP { get; set; }
	public int CritChance { get; set; }
	public string Race { get; set; }
	public string Klass { get; set; }

	public bool UsedSkill 
	{ 
		get 
		{
			return usedSkill; 
		}
		set 
		{
			usedSkill = false;
			if (!ReferenceEquals(this, Program.Player))
			{ 
			  usedSkill = true; //ik wil niet dat enemies op beurt 1 een skill kunnen gebruiken
			}
		}
	}

	
    List<int> levelBooster; //bepaalt met hoevel de stats omhoog gaan per level


	public Entity()
	{ }
	public Entity(string klass, string race, int healthIn, int damageIn, int levelIn, string name)
	{
		switch (race)
		{
			case "Mens":
				levelBooster = new List<int>() { 3, 3 }; // balanced
				break;
			case "Orc":
				levelBooster = new List<int>() { 4, 2 }; // veel hp minder damage
				break;
			case "Elf":
				levelBooster = new List<int>() { 2, 4 }; // laag hp veel damage
				break;
			default:
				levelBooster = new List<int>() { 1, 1 }; 
				break;
		}


		Level = levelIn;
		Name = name;
		Race = race;
		Klass = klass;
		CritChance = 1;
		Health = healthIn + (levelBooster[0] * Level);
		maxHealth = Health;
		Damage = damageIn + (levelBooster[1] * Level);
		XP = 0;
	}





	public void EncounterTriggered()
	{
		Program.Player.UsedSkill = false;
	}



	public static void Gunslinger()
	{
		Program.Player.Klass = "Gunslinger";
		Program.menu = false;
    }

	public static void Samurai()
	{ 
		Program.Player.Klass = "Samurai";
        Program.menu = false;


    }

    public static void Fighter()
	{ 
		Program.Player.Klass = "Fighter";
        Program.menu = false;
    }


    public static void Mens()
    {
		Program.Player.Race = "Mens";
        Program.menu = false;

    }

    public static void Elf()
	{
		Program.Player.Race = "Elf";
        Program.menu = false;

    }

    public static void Orc()
    {
        Program.Player.Race = "Orc";
        Program.menu = false;

    }

}