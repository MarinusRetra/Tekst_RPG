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

    public static void StartCombat(List<Entity> opponent)
    {
        isPlayerTurn = true;
        Enemy = opponent.ElementAt(random.Next(0, opponent.Count-1));//kan alles pakken behalve de laatste van de list


    CombatLoop:
        if (isPlayerTurn == true)
        {
            Console.Clear();
            ShowCombatStat(false);
            PlayerChoices.Selector("Attack", "Guard", "Use Skill", 0, typeof(Combat));

        }

//--------------------------------------------Enemy actions vv

        if (isPlayerTurn == false && Enemy.Health > 0)
        {
            select = random.Next(1, 4);
            if(Enemy.SkillCD == 0)
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
            Enemy.SkillCD--;
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
            Console.Clear();
            Console.WriteLine($"{Enemy.Name} is verslagen!");
            opponent.Remove(Enemy);
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
        if (user.SkillCD != 0)
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
          
          user.usedSkill = true;
          switch (user.Klass)
          {
              case "Gunslinger": // doe twee attacks in 1 beurt
                  Attack(user, target);
                  Thread.Sleep(700);
                  Attack(user, target);
                  ShowCombatStat();
                  user.SkillCD = 3;
              break;
          
              case "Samurai":// geeft 50% van je maximum health terug en je valt aan
                  user.Health += user.maxHealth / 2;
                  Attack(user, target);
                  user.SkillCD = 4;
                  break;
          
          
              case "Fighter": // Reflecteer de enemy skill als die gebruikt wordt
                  if (target.UsedSkill)
                  {
                      switch (target.Klass) //pakt de enemy klass gebruikt de skill op de enemy met de enemy stats
                      {
                      case "Gunslinger":
                          Attack(target, target);
                          Thread.Sleep(700);
                          Attack(target, target);
                          break;
          
                      case "Samurai":
                          target.Health -= target.maxHealth / 2; //invert de samurai skill en doet de helft damage op de gebruiker
                          ShowCombatStat();
                          break;
          
                      case "Fighter": 
                          ShowCombatStat();
                          break;
          
                      default:
                          Console.WriteLine("Reflectie van skill vaalt");
                          ShowCombatStat();
                          user.SkillCD = 2;
                          break;
                      }
                  }
                  break;
          }
        }
    }

    public static void Attack(Entity user = null, Entity target = null)
    {
        Console.SetCursorPosition(0, 7);
        Console.WriteLine($"{user.Name} valt aan");
        Thread.Sleep(1000);
        if (target == null || user == null)
        {
            Enemy.Health -= random.Next(Player.Damage, Player.Damage * 2);
        }
        else
        {
            target.Health -= random.Next(user.Damage, user.Damage * 2);
        }
        ShowCombatStat();
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
        Console.WriteLine($"{Enemy.Name} : Health {Enemy.Health}");
        Console.WriteLine($"{Player.Name} : Health {Player.Health} : SkillCD {Player.SkillCD}");
        Console.SetCursorPosition(0, 0);
        if (swapTurn)
        {
            isPlayerTurn = !isPlayerTurn;
            PlayerChoices.menu = isPlayerTurn;
        }
    }
}
