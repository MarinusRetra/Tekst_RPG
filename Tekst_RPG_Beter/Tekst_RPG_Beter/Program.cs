using System.Reflection;
namespace Tekst_RPG_Beter
{
    internal class Program
    {
        public static bool menu = true;

        public static Entity Player = new Entity("", "", 100, 20, 0, "");

        static void Main(string[] args)
        {
            // de laatste parameter is nodig om te kiezen van welke script hij de functies pakt
            Selector("Start", "Options", "Quit" , 0 , typeof(Program));
        }


        public static void Selector(string function1, string function2, string function3, int cursorStart , Type insertType)
        {
            Console.WriteLine(function1);
            Console.WriteLine(function2);
            Console.WriteLine(function3);


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


            Console.WriteLine();
            menu = true;
            Console.SetCursorPosition(0, cursorStart); // deze zet de cursor op de Y as

            int selected = Console.GetCursorPosition().Top; // pakt de curser position
            int startPos = selected; //pakt de startpositie van de cursor

            while (menu == true)
            {
                ConsoleKey read = Console.ReadKey().Key;

                MethodInfo method1 = insertType.GetMethod(function1);// dit pakt de functie met dezelfde naam als de string paramater function 1
                MethodInfo method2 = insertType.GetMethod(function2);// dit pakt ook de functie met dezelfde naam als de string paramater function 2
                MethodInfo method3 = insertType.GetMethod(function3);// ik denk dat je kan raden wat deze lijn doet


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
                    switch (trueSelected)// invoked de method op basis van welke string geselecteerd is
                    {
                        case (0):
                            method1.Invoke(null, null);
                            break;
                        case (1):
                            method2.Invoke(null, null);
                            break;
                        case (2):
                            if (function3 == "UseSkill")
                            {
                             method3.Invoke(null, new object[] {Player, Combat.Enemy}); 
                             // roept de functie met paramaters aan als het de use skill functie is
                            }
                            else
                            { 
                             method3.Invoke(null, null);
                            }
                            break;
                    }
                }
            }
        }

        public static void Start() // De code in deze functie geeft de speler een character creator
        {
            Console.Clear();
            Console.Write($"Dit is het verhaal van: ");
            Player.Name = Console.ReadLine().ToString();

            Console.WriteLine($"{Player.Name} is een ");
            Console.WriteLine();

            Selector("Orc", "Elf", "Mens" , 3, typeof(Entity));


            Console.Clear();
            Console.WriteLine($"Natuurlijk is {Player.Name} {"de"} {Player.Race.ToLower()} {"een: "} " );
            Console.WriteLine();

            Selector("Gunslinger" ,"Fighter" , "Samurai" , 2 , typeof(Entity));

            Console.Clear();
            Console.WriteLine($"{Player.Name} {Player.Race.ToLower()} {Player.Klass.ToLower()} klint goed");
            Thread.Sleep(1000);

            Console.WriteLine("Dat was alles veel plezier");
            Thread.Sleep(1500);


            Console.Clear();

            Print($"{Player.Name}. Tijdens een bank overval werdt je betrapt en nu zit je vast. \nNa maanden graven heb je een geheime gang net buiten je cel kunnen graven.\nJe besluit om:");
            Console.WriteLine();
            Selector("De tunnel in te gaan", "Een sleutel te stelen" ,"Opnieuw te beginnen" , 3 , typeof(Program) );


            Console.ReadLine();
        }

        public static void Options()
        { 
          Console.WriteLine("Er is jammergenoeg nog niets om in te stellen");
        }

        public static void Quit()
        {
          Environment.Exit(0);
        }

        //playerchoices
        //---------------------------------------------------------------------------------------------------------------------------------
        //maak deze later een eigen class

        public static void Detunnelintegaan()
        {
            Console.Clear();
            Print("Je besluit de tunnel in te gaan en komt nu midden in de gang uit. Een bewaker ziet je staan en komt op je af.");
            Combat.StartCombat(Zones.StartEnemies);
        }

        public static void Eensleuteltestelen()
        {
            Console.Clear();
            Print("Je komt voorzichtig dicht bij de cel bewaker en pakt door de tralies heen zijn sleutelbos van zijn riem af. \n Je gebruikt de sleutels om de deur te openen en de bewaker te verassen");
            Combat.StartCombat(Zones.StartEnemies);
        }

        public static void Opnieuwtebeginnen()
        { 
            Console.Clear();
            Start();
        }








        //---------------------------------------------------------------------------------------------------------------------------------









        public static void Print(string print, int speed = 5) // lagere speed is sneller
        {
            print.ToCharArray();
            foreach (char c in print) 
            { 
             Console.Write(c);
             Thread.Sleep(speed);
            }
        }
    }
}