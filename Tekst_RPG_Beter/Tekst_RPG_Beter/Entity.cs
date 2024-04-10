public class Entity
{

	private int health;
	static Random ran = new Random();

    public int maxHealth;
	public bool Deflecting { get; set; }
	public string Name { get; set; }
	public int Health {
		get { return health; }
		
		set { health = Math.Min(value, maxHealth);  }
		// de set pakt altijd het laagste getal tussen 0 en maxhealth, als je een hogere value hebt dan maxHealht capped het op maxHealth
	}
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
				levelBooster = new List<int>() { 6, 3}; // balanced 
				break;
			case "Orc":
				levelBooster = new List<int>() {7, 2}; // veel hp minder damage
				break;
			case "Elf":
				levelBooster = new List<int>() { 5, 4}; // laag hp veel damage
				break;
			default:
				levelBooster = new List<int>() { 30, 7};
				break;
		}

		Level = levelIn;
		Name = name;
		Race = race;
		Klass = klass;
		maxHealth = healthIn + (levelBooster[0] * Level);
		health = maxHealth;
		Damage = damageIn + (levelBooster[1] * Level);
		SkillCD = 0;
		XP = 0;
		PotionAmount = 1;
		XP_To_Give += (Health + Damage) * Level;
	}

	public static void CheckLevelUpAndSetNextMilestone(Entity EnemyDefeated)
	{
        Console.Clear();
        int milestoneXP = PlayerChoices.Player.XP + 400 * PlayerChoices.Player.Level; // zet de benodigde xp voor het volgende level
        PlayerChoices.Player.XP += EnemyDefeated.XP_To_Give; // voegt enemy xp to give aan de speler
        Console.WriteLine($"{EnemyDefeated.Name} is defeated!  currentXP: {PlayerChoices.Player.XP} XP / {milestoneXP}"); // vertelt hoeveel xp je nog nodig hebt

		if (PlayerChoices.Player.XP >= milestoneXP) // als je over de milestoneXP zit dan ga je een level omhoog
		{
			PlayerChoices.Player.Level++;
            PlayerChoices.Player.maxHealth = PlayerChoices.Player.maxHealth + PlayerChoices.Player.levelBooster[0] * PlayerChoices.Player.Level;
            PlayerChoices.Player.Damage = PlayerChoices.Player.Damage + PlayerChoices.Player.levelBooster[1] * PlayerChoices.Player.Level;
			// ^^ zet de damage en maxHealth naar levelbooster[0 voor health en 1 voor damage] * level + player damage of player maxhealth 

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