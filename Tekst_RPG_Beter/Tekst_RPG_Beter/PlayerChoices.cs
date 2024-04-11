using System.Reflection;
using Tekst_RPG_Beter;
using System;

public static class PlayerChoices
{
    public static Entity Player = new Entity("", "", 150, 35, 0, "");
    public static bool menu = true;

    /// <summary>
    /// Pakt de functies met dezelfde naam als de eerste drie parameters en maakt hier een klein menu van.
    /// Reset als de gebruiker probeert te typen
    /// </summary>
    /// <param name="function1"> Naam van functie als string</param>
    /// <param name="function2"> Naam van functie als string</param>
    /// <param name="function3"> Naam van functie als string</param>
    /// <param name="cursorStart"> De begin locatie van de cursor Y positie</param>
    /// <param name="insertType"> Een reference naar de class waarvan je de functies wilt gebruiken: typeof(Class naam)</param>
    public static void Selector(string function1, string function2, string function3, int cursorStart, Type insertType)
    {
    NoPrint:
       
        if (Combat.Enemy != null)
        {
            Combat.ShowCombatStat(false);
        }
        Console.SetCursorPosition(0, cursorStart);

        Console.CursorVisible = true;
        Instelbaar.Print(function1);
        Instelbaar.Print(function2);
        Instelbaar.Print(function3);



        Console.WriteLine();
        menu = true;
        Console.SetCursorPosition(0, cursorStart); // deze zet de cursor op de Y as

        int selected = Console.GetCursorPosition().Top; // pakt de curser position
        int startPos = selected; //pakt de startpositie van de cursor

        while (menu == true)
        {
            ConsoleKey read = Console.ReadKey().Key;
            if (read != ConsoleKey.Enter && read != ConsoleKey.UpArrow && read != ConsoleKey.DownArrow)
            {// reset de tekst als er niet enter uparrow of downarrow ingedrukt wordt
                Console.Clear();
                goto NoPrint;
            }

            if (read == ConsoleKey.DownArrow && Console.CursorTop != Console.WindowHeight - 1)
            {
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
            // deze twee if's zorgen voor dat je niet het programma kan crashen door uit de border te gaan en dat je door het menu
            // kan navigeren
            if (read == ConsoleKey.UpArrow && Console.CursorTop != Console.WindowTop)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }

            selected = Console.GetCursorPosition().Top;
            int trueSelected = selected - startPos;
            //true selected is een aangepaste versie van selected die ervoor
            //zorgt dat het tellen van boven naarbeneden altijd
            //bij 0 start los van waar de curser nu is gestart op de console

            if (trueSelected >= 3)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            // deze twee if statements zorgen ervoor dat je niet uit de selectie box kan

            else if (trueSelected <= -1)
            {
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }


            if (read == ConsoleKey.Enter && menu == true)
            // roept de functie die als paramaters die zijn ingevoerd
            // en met Y van de cursor positie kiest hij welke van de drie geselecteerd is
            {
                // 25 tot 32 is voor het verwijderen van spaties tussen de woorden van de input
                // dit heb ik gedaan zodat ik meerdere woorden als optie aan de speler kan laten zien
                // zonder_het_zoals_dit_te_moeten_schrijven,
                // omdat de function1/2/3 strings worden gebruikt om een functie met dezelfde naam aan te roepen
                string[] v = function1.Split(' ');
                function1 = string.Join("", v);
                
                string[] vl = function2.Split(' ');
                function2 = string.Join("", vl);
                
                string[] vla = function3.Split(' ');
                function3 = string.Join("", vla);

                MethodInfo method1 = insertType.GetMethod(function1);// dit pakt de functie met dezelfde naam als de string paramater function 1
                MethodInfo method2 = insertType.GetMethod(function2);// dit pakt ook de functie met dezelfde naam als de string paramater function 2
                MethodInfo method3 = insertType.GetMethod(function3);// ik denk dat je kan raden wat deze lijn doet

                switch (trueSelected)// invoked de method op basis van welke string geselecteerd is
                { // de if statements in de switch cases zijn voor specifieke functies die parameters doorgeven
                    case (0):
                        if (function1 == "Attack")
                        { 
                            method1.Invoke(null, new object[] {Player, Combat.Enemy, false});
                        }
                        else
                            method1.Invoke(null, null);
                    break;
                    case (1):
                        if (function2 == $"DrinkPotion[{Player.PotionAmount}]")
                        {
                            function2 = "DrinkPotion";
                            method2 = insertType.GetMethod("DrinkPotion");
                            method2.Invoke(null, new object[] { Player });
                        }
                        else
                            method2.Invoke(null, null);
                    break;
                    case (2):
                        if (function3 == "UseSkill")
                        {
                            method3.Invoke(null, new object[] {Player, Combat.Enemy});
                            // roept de functie met paramaters aan als het de use skill functie is
                        }
                        else
                            method3.Invoke(null, null);
                    break;
                }
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    ///  De code in deze functie geeft de speler een character creator en start vervolgens het spel
    /// </summary>
    public static void Start()//Startmenu keuze 
    {
        Console.Clear();
        Instelbaar.Print($"This is the story of: ",false);
        Player.Name = Console.ReadLine();
        Player.Name = string.IsNullOrEmpty(Player.Name) ? "Nameless Prisoner" : Player.Name;// je naam wordt nameless prisoner als je geen naam kiest

        Instelbaar.Print($"{Player.Name} is an");
        Console.WriteLine();

        Selector("Orc", "Elf", "Human", 3, typeof(Entity));

        Console.Clear();
        Instelbaar.Print($"Of course {Player.Name} {"the"} {Player.Race.ToLower()} {"is a: "} ");
        Console.WriteLine();

        Selector("Gunslinger", "Fighter", "Samurai", 2, typeof(Entity));
        Console.Clear();
        Instelbaar.Print($"{Player.Name} the {Player.Race.ToLower()} {Player.Klass.ToLower()}, sounds good");
        Thread.Sleep(800);
        Instelbaar.Print("That's all, good luck!");
        Thread.Sleep(800);
        Console.Clear();

        Instelbaar.Print($"{Player.Name}. During a bank robbery you got caught and are now in prison. \nAfter months of digging you made yourself a small tunnel that leads into the hallway just outside your cell.\nYou decide to:");
        Console.WriteLine();
        Selector("Enter the tunnel", "Try to steal a key", "Restart", 4, typeof(PlayerChoices)); // eerste playerkeuze

        Console.ReadLine();
        Room.GoThroughPrisonRooms();
        Room.GoThroughForestRooms();
    }

    public static void Options()//Startmenu keuze
    {
        Console.Clear();
        Selector("SetTekstSpeed","SetDifficulty","Back",0, typeof(Instelbaar));
    }

    public static void Quit()//Startmenu keuze
    {
        Environment.Exit(0);
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    public static void Enterthetunnel()// eerste player keuze
    {
        Console.Clear();
        Instelbaar.Print("You crawl through the small hole in the ground and end up in the middle of the hallway between cells. A guard saw you escape and is rapidly approuching you!");
        Console.ReadLine();
        Combat.StartCombat(Zones.StartEnemies[0]);
    }

    public static void Trytostealakey()// eerste player keuze
    {
        Console.Clear();
        Instelbaar.Print("You sneakily get closer to the cell bars until you're in range to free the guards key from his belt. \nYou use the key to release yourself and take the gaurd by surprise");
        Console.ReadLine();
        Combat.StartCombat(Zones.StartEnemies[1]);
    }

    public static void Restart()// eerste player keuze
    {
        Console.Clear();
        Start();
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

}
