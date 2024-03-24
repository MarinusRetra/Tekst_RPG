using System;

public class Instelbaar
{
    int textSpeed = 0; 

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
