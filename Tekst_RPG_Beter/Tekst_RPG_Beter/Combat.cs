using System;
using Tekst_RPG_Beter;

public static class Combat
{
    public static int Give_XP { get; set; }

    static Random random = new Random();

    public static Entity Enemy;

    static Entity Player = PlayerChoices.Player;

    static bool isPlayerTurn = true;

    static int select = 0;

    //startCombat pakt een list met Entities en in die list zitten de enemies die in elke zone kunnen verschijnen

    public static void StartCombat(Entity opponent)
    {
        if (Player.Health <= Player.maxHealth - Player.maxHealth / 100 * 25)//de speler healt voor elke combat die gestart is terwijl de player minimaal 25 procent hp mist
        {
            Player.Health += Player.maxHealth / 100 * 25;
        }
        isPlayerTurn = true;
        Player.SkillCD = 0;
        Enemy = opponent;


    CombatLoop:
        if (isPlayerTurn == true)
        {
            Player.Deflecting = false;
            // Console.Clear();
            //ShowCombatStat(false);
            PlayerChoices.Selector("Attack", $"DrinkPotion[{Player.PotionAmount}]", "Use Skill", 0, typeof(Combat));
            if (Player.SkillCD > 0)
            { 
                Player.SkillCD--;
            }

        }

//--------------------------------------------Enemy actions vv

        if (isPlayerTurn == false && Enemy.Health > 0)
        {
            Enemy.Deflecting = false;

            select = random.Next(1, 3); // welke actie de enemy gaat doen wordt hier gekozen
            
            if(Enemy.SkillCD == 0)// de enemy doet altijd zijn skill op het moment dat hij die krijgt
            {
                select = 3;
            }

            switch (select)
            {
                case 1:
                    Attack(Enemy, Player, false);
                    break;

                case 2:
                    if (Enemy.PotionAmount > 0 && Enemy.Health < Enemy.maxHealth / 2)
                    {// drinkt alleen een potion als er nog potions zijn en als health minder is dan maxhealth / 2
                        DrinkPotion(Enemy);
                    }
                    else
                    { 
                        Attack(Enemy, Player, false);
                    }
                break;

                case 3:
                    UseSkill(Enemy,Player);
                break;

                default:
                throw new Exception("Er gaat iets fout in combat");
            }

            if (Enemy.SkillCD > 0)
            {
              Enemy.SkillCD--;
            }
        }
//--------------------------------------------Enemy actions ^^

//--------------------------------------------Bepaalt of combat doorgaat of stopt vvv
        if (Player.Health <= 0)
        {
            PlayerChoices.menu = false;
            Console.Clear();
            Console.WriteLine("You stierf");
            Console.ReadLine();
            Environment.Exit(0);
        }

        if (Enemy.Health <= 0)
        {
            PlayerChoices.menu = false;
            if (Enemy.PotionAmount > 0)
            {
                Console.Clear();
                Instelbaar.Print("You defeated the enemy before they could use their potion!\n +1 Potion");
                Player.PotionAmount++;
                Console.ReadLine();
            }
            Entity.CheckLevelUpAndSetNextMilestone(Enemy);
            Zones.FindList(Enemy, Zones.listList).Remove(Enemy);
        }
        if (Enemy.Health > 0 && Player.Health > 0)
        {
            goto CombatLoop;
        }
//--------------------------------------------Bepaalt of combat doorgaat of stopt ^^^
    }

    public static void UseSkill(Entity user, Entity target)
    {
          if (user.SkillCD > 0 && isPlayerTurn)
          {
             //stop de functie als SkillCD niet null is en ga terug naar het menu
             Console.WriteLine("Je skill is op cooldown");
             Console.SetCursorPosition(0, 2);
          }
          else
          { 
             Console.SetCursorPosition(0, 7);
             Console.WriteLine($"{user.Name} gebruikt de {user.Klass} skill");
             Thread.Sleep(700);
          
            if (!target.Deflecting)
            {
                switch (user.Klass)
                {
                    // doe twee attacks in 1 beurt
                    case "Gunslinger": 
                        user.SkillCD = 3;
                        Attack(user, target, false);
                        Thread.Sleep(700);
                        Attack(user, target, false);
                        ShowCombatStat();
                    break;

                    // geeft 1/3 van je maximum health terug en je valt aan
                    case "Samurai":
                        user.Health += user.maxHealth / 3;
                        user.SkillCD = 4;
                        Attack(user, target);
                    break;

                    // Reflecteer de enemy skill als die gebruikt wordt na jouw beurt
                    case "Fighter": 
                        user.SkillCD = 2;
                        user.Deflecting = true;
                        ShowCombatStat();
                    break;
                }
            }
            else //als deflect true is
            {
                // pakt de enemy klass gebruikt de skill op de enemy met de enemy stats
                switch (user.Klass) 
                {
                    case "Gunslinger":
                        Attack(user, user, false);
                        Thread.Sleep(700);
                        Attack(user, user, false);
                        ShowCombatStat();
                        user.SkillCD = 3;

                    break;

                    case "Samurai":
                        Attack(user, user);
                        target.Health += target.maxHealth / 3;
                        user.SkillCD = 4;
                    break;

                    case "Fighter":
                        user.SkillCD = 2;
                        target.SkillCD = 2;
                        Console.WriteLine("Both of stand ready to counterattack. But no one moves");
                        Thread.Sleep(1000);
                        ShowCombatStat();
                    break;

                    default:
                        Console.WriteLine("Reflectie van skill vaalt");
                        ShowCombatStat();
                    break;
                }
            }
         }
    }

    public static void Attack(Entity user = null, Entity target = null, bool dontPrintStuff = true)
    {
        Console.SetCursorPosition(0, 7);
        Console.WriteLine(dontPrintStuff ? "" : $"{user.Name} valt aan met zijn {user.Klass} moves");
        target.Health -= random.Next(user.Damage, user.Damage * 2);
        Thread.Sleep(1000);
        ShowCombatStat();
    }

    public static void DrinkPotion(Entity user)
    {
        if (user.PotionAmount > 0)
        {
            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"{user.Name} drinks a potion");
            user.Health += user.maxHealth / 2;
            user.PotionAmount--;
            Thread.Sleep(1000);
            ShowCombatStat();
        }
        else
        {
            Console.Write("You're out of potions");
        }
    }

    /// <summary>
    /// Geeft alle stats en tekst die de player moet zien tijdens combat op het scherm en wisselt de beurt.
    /// </summary>
    public static void ShowCombatStat(bool swapTurn = true)
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"Attack\nDrinkPotion[{PlayerChoices.Player.PotionAmount}]\nSkill");
        Console.SetCursorPosition(0, 4);
        Console.WriteLine($"Level {Enemy.Level} {Enemy.Name} : Health {Enemy.Health}");
        Console.WriteLine($"{Player.Name} : Health {Player.Health} : SkillCD {Player.SkillCD}");
        Console.SetCursorPosition(0, 0);
        if (swapTurn)
        {
            isPlayerTurn = !isPlayerTurn;
            PlayerChoices.menu = isPlayerTurn;
        }
        Thread.Sleep(200);
    }

   




}
