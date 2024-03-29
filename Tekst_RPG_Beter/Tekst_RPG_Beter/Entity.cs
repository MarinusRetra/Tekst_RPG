using System;
using System.Runtime.CompilerServices;
using Tekst_RPG_Beter;

public class Entity
{
	public int skillCD;
    public int maxHealth;
	public bool Deflecting { get; set; }
	public string Name { get; set; }
	public int Health { get; set; }
	public int Damage { get; set; }
	public int Level { get; set; }
	public int XP { get; set; }
	public int CritChance { get; set; }
	public string Race { get; set; }
	public string Klass { get; set; }
	public string XP_To_Give { get; set; }


    public int SkillCD 
	{ 
		get 
		{
			return skillCD; 
		}
		set 
		{
			skillCD = 0;
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
				levelBooster = new List<int>() { 15, 5 }; // balanced
				break;
			case "Orc":
				levelBooster = new List<int>() { 20, 3 }; // veel hp minder damage
				break;
			case "Elf":
				levelBooster = new List<int>() { 10, 7 }; // laag hp veel damage
				break;
			default:
				levelBooster = new List<int>() { 10, 10 }; 
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
		XP_To_Give += Health + Damage * Level;
		skillCD = SkillCD;
	}


	public static void Gunslinger()
	{
		PlayerChoices.Player.Klass = "Gunslinger";
		PlayerChoices.menu = false;
    }

	public static void Samurai()
	{ 
		PlayerChoices.Player.Klass = "Samurai";
        PlayerChoices.menu = false;
    }

    public static void Fighter()
	{ 
		PlayerChoices.Player.Klass = "Fighter";
        PlayerChoices.menu = false;
    }


    public static void Mens()
    {
		PlayerChoices.Player.Race = "Mens";
        PlayerChoices.menu = false;

    }

    public static void Elf()
	{
		PlayerChoices.Player.Race = "Elf";
        PlayerChoices.menu = false;

    }

    public static void Orc()
    {
        PlayerChoices.Player.Race = "Orc";
        PlayerChoices.menu = false;

    }

}