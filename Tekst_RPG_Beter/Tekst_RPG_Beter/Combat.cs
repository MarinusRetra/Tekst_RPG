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
        isPlayerTurn = true;
        Player.SkillCD = 0;
        Enemy = opponent;


    CombatLoop:
        if (isPlayerTurn == true)
        {
            Player.Deflecting = false;
            Console.Clear();
            ShowCombatStat(false);
            PlayerChoices.Selector("Attack", "Guard", "Use Skill", 0, typeof(Combat));
            if (Player.SkillCD > 0)
            { 
                Player.SkillCD--;
            }

        }

//--------------------------------------------Enemy actions vv

        if (isPlayerTurn == false && Enemy.Health > 0)
        {
            Enemy.Deflecting = false;

            select = random.Next(1, 3); // welke actie de enemy gaat doen op zijn beurt attack of guard
            
            if(Enemy.SkillCD == 0)// de enemy doet altijd zijn skill op het moment dat hij die krijgt
            {
                select = 3;
            }
            
            switch (select)
            {
                case 1:
                    Attack(Enemy, Player);
                break;

                case 2:
                    Guard(Enemy);
                break;

                case 3:
                    UseSkill(Enemy,Player);
                break;

                default:
                    ShowCombatStat();
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
            Console.WriteLine("You died");
            Console.ReadLine();
        }

        if (Enemy.Health <= 0)
        {
            PlayerChoices.menu = false;
            Entity.CheckLevelUpAndSetNextMilestone(Player, Enemy);
            Zones.FindList(Enemy, Zones.listList).Remove(Enemy);
            Console.ReadLine();
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
                        Attack(user, target, false);
                        Thread.Sleep(700);
                        Attack(user, target, false);
                        ShowCombatStat();
                        user.SkillCD = 3;
                    break;

                    // geeft 50% van je maximum health terug en je valt aan
                    case "Samurai":
                        user.Health += user.maxHealth / 2;
                        Attack(user, target);
                        user.SkillCD = 4;
                    break;

                    // Reflecteer de enemy skill als die gebruikt wordt na jouw beurt
                    case "Fighter": 
                        user.Deflecting = true;
                        ShowCombatStat();
                        user.SkillCD = 2;
                    break;
                }
            }
            else
            {
                // pakt de enemy klass gebruikt de skill op de enemy met de enemy stats
                switch (user.Klass) 
                {
                    case "Gunslinger":
                        Attack(user, user, false);
                        //Thread.Sleep(1000);
                        Attack(user, user, false);
                        user.SkillCD = 3;
                    break;

                    case "Samurai":
                        user.Health -= user.maxHealth / 2; 
                        ShowCombatStat();
                        user.SkillCD = 4;
                    break;

                    case "Fighter":
                        Console.WriteLine("Both of stand ready to counterattack. Nothing Happens");
                        ShowCombatStat();
                        user.SkillCD = 2;
                        target.SkillCD = 2;
                    break;

                    default:
                        Console.WriteLine("Reflectie van skill vaalt");
                        ShowCombatStat();
                    break;
                }
            }
         }
    }

    public static void Attack(Entity user = null, Entity target = null, bool printStuff = true)
    {
        Console.SetCursorPosition(0, 7);
        Console.WriteLine(printStuff ? "" : $"{user.Name} valt aan met zijn {user.Klass} moves");
        target.Health -= random.Next(user.Damage, user.Damage * 2);
        ShowCombatStat();
        Thread.Sleep(1000);
    }

    public static void Guard(Entity user)
    {
        Console.SetCursorPosition(0, 7);
        Console.WriteLine($"{user.Name} verdedigd");
        Thread.Sleep(1000);
        if (ReferenceEquals(Player, user))
        {
            user.Health += user.maxHealth / 4;
        }
        else 
        {
            user.Health += user.maxHealth / 6;
        }
        ShowCombatStat();
        //de speler krijgt meer health per guard actie, dit is zodat de enemy niet sneller kan healen dan de speler en een onmogelijke battle maakt
    }

    /// <summary>
    /// Geeft alle stats die de player moet zien tijdens combat op het scherm en wisselt de beurt.
    /// </summary>
    public static void ShowCombatStat(bool swapTurn = true)
    { 
        Console.SetCursorPosition(0, 4);
        Console.WriteLine($"Level {Enemy.Level} {Enemy.Name} : Health {Enemy.Health}");
        Console.WriteLine($"{Player.Name} : Health {Player.Health} : SkillCD {Player.SkillCD}");
        Console.SetCursorPosition(0, 0);
        if (swapTurn)
        {
            isPlayerTurn = !isPlayerTurn;
            PlayerChoices.menu = isPlayerTurn;
        }
    }

   




}
