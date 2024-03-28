using System;
using Tekst_RPG_Beter;

public class Instelbaar
{
    static int tekstSpeed;
    static int difficulty;

    public static int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value > 0 && value < 3 ? difficulty = value : 1; }
    }   // zolang de waarde hoger is dan 0 en lager dan 3 wordt de waarde gezet, zo niet, dan wordt die 1

    public static int TekstSpeed 
      {
        get { return tekstSpeed; } 
        set { tekstSpeed = value > 0 && value < 101 ? tekstSpeed = value : 5;} 
      } // zolang de waarde hoger is dan 0 en lager dan 100 wordt de waarde gezet, zo niet, dan wordt die 5 
    
    public static void Print(string print) // lagere speed is sneller
    {
        int speed = tekstSpeed;
        print.ToCharArray();
        foreach (char c in print)
        {
            Console.Write(c);
            Thread.Sleep(speed);
        }
        Console.WriteLine();
    }

    public static void SetTekstSpeed()
    {
        Console.Write("Kies een tekstsnelheid tussen 0 en 101 :    (Je voert in hoeveel milliseconden geprint worden tussen elke letter)");
        Console.SetCursorPosition(41,0);
        if (int.TryParse(Console.ReadLine(), out int speed))
        {
            TekstSpeed = speed;
        }
        Console.Clear();
        PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));
    }

    public static void SetDifficulty()
    {
        Console.Write("Kies difficulty tussen 1 of 2 :    (1 is normaal 2 is moeilijk)");
        Console.SetCursorPosition(33, 1);
        if (int.TryParse(Console.ReadLine(), out int DifficultyMultiplier))
        {
            Difficulty = DifficultyMultiplier;
        }
        Console.Clear();
        PlayerChoices.Selector("Start", "Options", "Quit", 0, typeof(PlayerChoices));
    }
}
