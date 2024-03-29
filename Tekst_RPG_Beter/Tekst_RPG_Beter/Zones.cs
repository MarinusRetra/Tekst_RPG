public static class Zones
{
   //  "Class", "Race", hp, damage, startLevel, crit, "naam"
   //Gunslinger  Orc    100 20          0        1    BaseOrc
   //Samurai     Mens   100 20          0        1    BaseMens
   //Fighter     Elf    100 20          0        1    BaseElf

    public static List<Entity> PrisonPeople = new List<Entity>();

    public static List<Entity> ForestPeople = new List<Entity>();

    public static List<Entity> StartEnemies = new List<Entity>();
    static Zones() 
    {
        //enemies eerste encounter
        StartEnemies.Add(new Entity("Gunslinger", "Mens", 70, 20, 2, "Surprised Guard"));
        StartEnemies.Add(new Entity("Gunslinger", "Mens", 40, 20, 2, "Stupid Guard"));
        StartEnemies.Add(new Entity("Gunslinger", "Mens", 60, 20, 2, "Clumsy Guard")) ;

        //eerste zone
        PrisonPeople.Add(new Entity("Fighter", "Orc", 100, 15, 0, "Spooked Guard")); //1F
        PrisonPeople.Add(new Entity("Samurai", "Elf", 70, 30, 0, "Elf Guard")); // 2
        PrisonPeople.Add(new Entity("Fighter", "Orc", 120, 10, 0, "Orc Guard")); // 3
        PrisonPeople.Add(new Entity("Gunslinger", "Mens", 30, 40, 0, "Watchtower Guard")); // 4
        PrisonPeople.Add(new Entity("Fighter", "Mens", 100, 20, 0, "Guard")); // 5
        PrisonPeople.Add(new Entity("Fighter", "Mens", 100, 20, 0, "Guard")); // 7
        PrisonPeople.Add(new Entity("Fighter", "Mens", 100, 20, 0, "Guard")); // 6
        PrisonPeople.Add(new Entity("Fighter", "Orc", 80, 10, 0, "Scared Guard")); // 8
        PrisonPeople.Add(new Entity("Gunslinger", "Elf", 70, 30, 0, "Elf Guard")); // 9
        PrisonPeople.Add(new Entity("Fighter", "Orc", 250, 30, 0, "Warden")); // 10

        //tweede zone
        ForestPeople.Add(new Entity("Gunslinger", "Elf", 120, 40, 5, "Elf Archer")); // 1
        ForestPeople.Add(new Entity("Gunslinger", "Elf", 120, 40, 5, "Elf Archer")); // 2
        ForestPeople.Add(new Entity("Fighter", "Elf", 150, 35, 5, "Elf Monk")); // 3
        ForestPeople.Add(new Entity("Fighter", "Elf", 140, 35, 5, "Elf Monk")); // 4
        ForestPeople.Add(new Entity("Samurai", "Elf", 120, 40, 5, "Stoic Elf")); // 5
        ForestPeople.Add(new Entity("Gunslinger", "Elf", 140, 50, 5, "Sword Wielding Archer")); // 6
        ForestPeople.Add(new Entity("Samurai", "Orc", 230, 60, 5, "Elf Wielding Orc")); // 7
        ForestPeople.Add(new Entity("Gunslinger", "Elf", 200, 60, 5, "Advanced Elf Archer")); // 8
        ForestPeople.Add(new Entity("Gunslinger", "Elf", 200, 60, 5, "Advanced Elf Archer")); // 9
        ForestPeople.Add(new Entity("Gunslinger", "Mens", 400, 50, 5, "Wandering Samurai")); // 10

        foreach (Entity entity in ForestPeople)
        {
            entity.Damage += entity.Health * Instelbaar.Difficulty;
            entity.Health += entity.Health * Instelbaar.Difficulty;
        }
        foreach (Entity entity in PrisonPeople)
        {
            entity.Damage += entity.Health * Instelbaar.Difficulty;
            entity.Health += entity.Health * Instelbaar.Difficulty;
        }
        foreach (Entity entity in StartEnemies)
        {
            entity.Damage += entity.Health * Instelbaar.Difficulty;
            entity.Health += entity.Health * Instelbaar.Difficulty;
        }
        
    }

}
