using System;
using Tekst_RPG_Beter;

public static class Combat
{

    public static int Give_XP { get; set; }
    public static int SkillCD { get; set; }


    static Random random = new Random();

    public static Entity Enemy;

    static Entity Player = Program.Player;

    static bool isPlayerTurn = true;

    static int select = 0;

    //startCombat pakt een list met Entities en in die list zitten de enemies die in elke zone kunnen verschijnen

    public static void StartCombat(List<Entity> opponent)
    {
        isPlayerTurn = true;
        Enemy = opponent.ElementAt(random.Next(0, opponent.Count-1));//kan alles pakken behalve de laatste van de list


    CombatLoop:
        if (isPlayerTurn)
        {
            Console.Clear();
            ShowCombatStat();
            Program.Selector("Attack", "Guard", "Use Skill", 0, typeof(Combat));
            isPlayerTurn = false;
            Program.menu = isPlayerTurn;
        }
        else // dit zijn de dingen die de enemy kan doen
        {
            select = random.Next(1, 4);
            switch (select)
            {
                case 1:

                    Player.Health -= random.Next(Enemy.Damage, Enemy.Damage * 2);
                    isPlayerTurn = true;
                    Program.menu = isPlayerTurn;

                    break;
                case 2:

                    isPlayerTurn = true;
                    Program.menu = isPlayerTurn;
                    break;
                case 3:
                    UseSkill(Player,Enemy);
                    Program.menu = isPlayerTurn;

                    break;

                default:
                    Console.WriteLine("In de enemy combat ging iets fout");

                    isPlayerTurn = true;
                    Program.menu = isPlayerTurn;
                    break;
            }
        }


        if (Player.Health <= 0)
        {
            Program.menu = false;
            Console.Clear();
            Console.WriteLine("You died");
            Console.ReadLine();
        }

        if (Enemy.Health <= 0)
        {
            Program.menu = false;
            Console.Clear();
            Console.WriteLine($"{Enemy.Name} was deafeated!");
            Console.ReadLine();
        }
        if (Enemy.Health > 0 || Player.Health > 0)
        {
            goto CombatLoop;
        }
    }


    public static void UseSkill(Entity user, Entity target)
    {
        switch (user.Klass)
        {
            case "Gunslinger":
                //Doe twee attacks in 1 beurt

                target.Health -= random.Next(user.Damage, user.Damage * 2);
                ShowCombatStat();

                Thread.Sleep(700);

                target.Health -= random.Next(user.Damage, user.Damage * 2);
                ShowCombatStat();
                break;

            case "Samurai":
                // Dodge de volgende attack en counter

                // if attackdetected
                target.Health -= random.Next(user.Damage, user.Damage * 2);
                ShowCombatStat();

                isPlayerTurn = true;
                Program.menu = isPlayerTurn;
                break;

         
            case "Fighter":
                // Reflecteer de enemy skill als die gebruikt wordt
                if (target.UsedSkill)
                {
                    switch (target.Klass) //pakt de enemy klass gebruikt de skill op de enemy met de enemy stats
                    {
                    case "Gunslinger":
                        target.Health -= random.Next(target.Damage, target.Damage * 2);
                        ShowCombatStat();
                        target.Health -= random.Next(target.Damage, target.Damage * 2);
                        ShowCombatStat();
                        break;

                    case "Samurai":
                        target.Health -= random.Next(target.Damage, target.Damage * 2);
                        isPlayerTurn = true;
                        ShowCombatStat();
                        break;

                    case "Fighter":
                        Console.WriteLine("Both of you try to intercept eachother. Nothing happens.");
                        ShowCombatStat();
                        break;

                    default:
                        ShowCombatStat();
                        isPlayerTurn = false;
                        break;
                    }

                }
                break;
        }
    }

    public static void Attack()
    {
        Enemy.Health -= random.Next(Player.Damage, Player.Damage * 2);
        ShowCombatStat();
        
    }

    public static void Guard()
    {
        if (isPlayerTurn)
        {
          Player.Health += Player.maxHealth / 4;
          //de speler krijgt meer health per guard, dit is zodat de enemy niet sneller keer kan healen dan de speler
        } 
        else 
        {
          Enemy.Health += Enemy.maxHealth / 6; 
        }
        ShowCombatStat();
    }

    /// <summary>
    /// Geeft alle stats die de player moet zien op het scherm.
    /// </summary>
    public static void ShowCombatStat()
    { 
        Console.SetCursorPosition(0, 4);
        Console.WriteLine($"{Enemy.Name} : Health {Enemy.Health}");
        Console.WriteLine($"{Player.Name} : Health {Player.Health}");
        Console.SetCursorPosition(0, 0);
    }
}
