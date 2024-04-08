﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Tekst_RPG_Beter;

public class Entity
{
	static Random ran = new Random();

    public int maxHealth;
	public bool Deflecting { get; set; }
	public string Name { get; set; }
	public int Health { get; set; }
	public int Damage { get; set; }
	public int Level { get; set; }
	public int XP { get; set; }
	public string Race { get; set; }
	public string Klass { get; set; }
	public int XP_To_Give { get; set; }
	public int PotionAmount { get; set; }
    public int SkillCD { get; set; }

    List<int> levelBooster; //bepaalt met hoevel de stats omhoog gaan per level


	public Entity()
	{ }
	public Entity(string klass, string race, int healthIn, int damageIn, int levelIn, string name)
	{
		switch (race)
		{
			case "Human":
				levelBooster = new List<int>() { 15, 4}; // balanced 
				break;
			case "Orc":
				levelBooster = new List<int>() { 25, 3}; // veel hp minder damage
				break;
			case "Elf":
				levelBooster = new List<int>() { 10, 5}; // laag hp veel damage
				break;
			default:
				levelBooster = new List<int>() { 30, 7};
				break;
		}

		Level = levelIn;
		Name = name;
		Race = race;
		Klass = klass;
		Health = healthIn + (levelBooster[0] * Level);
		maxHealth = Health;
		Damage = damageIn + (levelBooster[1] * Level);
		SkillCD = 0;
		XP = 0;
		PotionAmount = 1;
		XP_To_Give += (Health + Damage) * Level;
	}

	public static void CheckLevelUpAndSetNextMilestone(Entity EnemyDefeated)
	{
        Console.Clear();
        PlayerChoices.Player.XP += EnemyDefeated.XP_To_Give; // voegt enemy xp to give aan de speler
        int milestoneXP = 300 * PlayerChoices.Player.Level; // zet de benodigde xp voor het volgende level
        Console.WriteLine($"{EnemyDefeated.Name} is defeated! + {EnemyDefeated.XP_To_Give}XP / {milestoneXP}"); // vertelt hoeveel xp je nog nodig hebt

		if (PlayerChoices.Player.XP >= milestoneXP) // als je over de milestoneXP zit dan ga je een level omhoog
		{
			PlayerChoices.Player.Level++;
            PlayerChoices.Player.maxHealth += PlayerChoices.Player.levelBooster[0] * PlayerChoices.Player.Level;
            PlayerChoices.Player.Damage += PlayerChoices.Player.levelBooster[1] * PlayerChoices.Player.Level;
			// ^^ zet de damage en maxHealth naar levelbooster[0 voor health en 1 voor damage] * level 

            Instelbaar.Print($"Level up! \nNew Level: {PlayerChoices.Player.Level} \nNew MaxHP: {PlayerChoices.Player.maxHealth} \nNew Damage: {PlayerChoices.Player.Damage}");
			// print je nieuwe stats naar de console
		}
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


    public static void Human()
    {
		PlayerChoices.Player.Race = "Human";
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