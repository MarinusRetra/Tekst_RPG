public static class Zones
{
   //  "Class", "Race", hp, damage, startLevel, crit, "naam"
   //Gunslinger  Orc    100 20          0        1    BaseOrc
   //Samurai     Mens   100 20          0        1    BaseMens
   //Fighter     Elf    100 20          0        1    BaseElf

    public static List<Entity> PrisonPeople = new List<Entity>();

    public static List<Entity> ForestPeople = new List<Entity>();

    public static List<Entity> StartEnemies = new List<Entity>();

    public static List<List<Entity>> listList = new List<List<Entity>> { PrisonPeople, ForestPeople, StartEnemies};
    // deze list wordt gebruikt om te kijken in welke list een entity object zit
    static Zones() 
    {
        //eerste paar enemies
        StartEnemies.Add(new Entity("Gunslinger", "Mens", 70, 20, 2, "Surprised Guard"));// level 2 100 hp, 30 dmg en geeft 260 xp
        StartEnemies.Add(new Entity("Gunslinger", "Mens", 40, 20, 2, "Stupid Guard")); //level 2 70 hp, 30 damage en geeft 200 xp
        StartEnemies.Add(new Entity("Gunslinger", "Mens", 60, 20, 2, "Clumsy Guard")) ; // level 2 90 hp, 30 damage en geeft 240 xp
        StartEnemies.Add(new Entity("Gunslinger", "Orc", 60, 20, 3, "Intimidating Guard")); // level 3 105 hp, 35 damage en geeft 280 xp

        //eerste zone
        PrisonPeople.Add(new Entity("Fighter", "Orc", 100, 15, 2, "Spooked Guard"));  //1
        Room.roomDescriptionsPrisonPeople.Add("As you make your way through the prison you see a guard that doesn't seem to be paying attention.\nYou sneak up on him before he spots you.");
       
        PrisonPeople.Add(new Entity("Samurai", "Elf", 70, 30, 3, "Elf Guard")); // 2
        Room.roomDescriptionsPrisonPeople.Add("You got lost and decide to ask for directions. As you aproach an elf wielding a sword you are quickly reminded to the fact that you're an escapee.");

        PrisonPeople.Add(new Entity("Fighter", "Orc", 120, 10, 3, "Orc Guard")); // 3
        Room.roomDescriptionsPrisonPeople.Add("A nimble orc comes chasing after you.");
        
        PrisonPeople.Add(new Entity("Gunslinger", "Mens", 70, 40, 3, "Watchtower Guard")); // 4
        Room.roomDescriptionsPrisonPeople.Add("As you step into the yard a shot barely misses your head. One of the guards in the watchtowers has spotted you!");

        PrisonPeople.Add(new Entity("Fighter", "Mens", 120, 20, 4, "Unfazed Guard")); // 5
        Room.roomDescriptionsPrisonPeople.Add("While making your way through one of the prison blocks you sneak up on a guard wearing boxing gloves.");

        PrisonPeople.Add(new Entity("Samurai", "Mens", 100, 20, 5, "Katana Wielding Guard")); // 6
        Room.roomDescriptionsPrisonPeople.Add("You peek around a corner and are greeted by a guard rapidly coming closer.");

        PrisonPeople.Add(new Entity("Fighter", "Mens", 100, 20, 3, "Guard")); // 7
        Room.roomDescriptionsPrisonPeople.Add("You've made your way into a darker area of the prison and are barely able to see, suddenly you feel someone bump into you.");
        
        PrisonPeople.Add(new Entity("Fighter", "Orc", 100, 12, 2, "Scared Guard")); // 8
        Room.roomDescriptionsPrisonPeople.Add("You have found yourself in the guard's lounge where an orc is reading 'What to do in case of an escape attempt for dummies'.");
        PrisonPeople.Add(new Entity("Gunslinger", "Elf", 70, 30, 4, "Elf Guard")); // 9
        Room.roomDescriptionsPrisonPeople.Add("You see a guard standing guard at the contraband desk, even though the room is locked and no one can get in, despite this the guard looks quite serious.");
        PrisonPeople.Add(new Entity("Fighter", "Orc", 250, 30, 8, "The Warden")); // 10
        Room.roomDescriptionsPrisonPeople.Add("The exit!                \nOne problem the warden seems to be blocking your way through. He is the only thing standing between you and the outside world.");

        
        //tweede zone
        ForestPeople.Add(new Entity("Gunslinger", "Elf", 120, 40, 8, "Elf Archer")); // 1
        Room.roomDescriptionsForestPeople.Add("While exploring the forest you are ambushed by an elf.");

        ForestPeople.Add(new Entity("Gunslinger", "Elf", 120, 40, 8, "Elf Archer")); // 2
        Room.roomDescriptionsForestPeople.Add("An arrows shoots in between your legs. An elf archer has spotted you!");

        ForestPeople.Add(new Entity("Fighter", "Elf", 150, 35, 12, "Elf Monk")); // 3
        Room.roomDescriptionsForestPeople.Add("You decided to climb up a tree to get a better view of the area, while climbing you get spoted bij an elf patrolling the area!");


        ForestPeople.Add(new Entity("Fighter", "Elf", 140, 35, 9, "Elf Monk")); // 4
        Room.roomDescriptionsForestPeople.Add("An elf monk yells out to from afar, challenging you to a duel.");
        ForestPeople.Add(new Entity("Samurai", "Elf", 120, 40, 12, "Stoic Elf")); // 5
        Room.roomDescriptionsForestPeople.Add("While trying to find food you are ambushed by a samurai!");

        ForestPeople.Add(new Entity("Gunslinger", "Elf", 140, 50, 11, "Sword Wielding Archer")); // 6
        Room.roomDescriptionsForestPeople.Add("As you try to find a higher vantage point you spot an elf equipped with a shortbow and a sword.");

        ForestPeople.Add(new Entity("Samurai", "Orc", 230, 60, 9, "Elf Wielding Orc")); // 7
        Room.roomDescriptionsForestPeople.Add("An enraged orc charges you wielding an elf.");

        ForestPeople.Add(new Entity("Gunslinger", "Elf", 200, 60, 11, "Advanced Elf Archer")); // 8
        Room.roomDescriptionsForestPeople.Add("While trying to exit this maze of a forest an elf jumps at you from the bushes");

        ForestPeople.Add(new Entity("Gunslinger", "Elf", 200, 60, 11, "Advanced Elf Archer")); // 9
        Room.roomDescriptionsForestPeople.Add("Its getting dark and you cannot see much, however you quickly discover that there's someone that can clearly see you.");
        ForestPeople.Add(new Entity("Gunslinger", "Mens", 400, 50, 15, "Wandering Ronin")); // 10
        Room.roomDescriptionsForestPeople.Add("You finally see an end to this forest, your joy fades quickly however when you realize you've been ambushed by a wandereing ronin.");


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

    /// <summary>
    /// Pakt de list van de ingegeven Entity uit een list met lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Enemy">Entity waarvan je de list wilt hebben</param>
    /// <param name="listLists">De doorzochte lists waarvan het object in kan zitten</param>
    /// <returns></returns>
    public static List<Entity> FindList(Entity Enemy, List<List<Entity>> listLists)
    {
        foreach (var list in listLists)
        {
            if (list.Contains(Enemy))
            {
                return list;
            }
        }
        Console.WriteLine("Niets gevonden");
        return null;
    }

}
