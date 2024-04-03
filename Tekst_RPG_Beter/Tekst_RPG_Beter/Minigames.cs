using System;
using System.Numerics;

public static class Minigames
{
    static Random rand = new Random();
    static void PrintBord(char[,] speelBord) // print het speelbord
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < speelBord.GetLength(0); i++)
        {
            // zorgt dat bij elke kolom op een nieuwe lijn staat
            if (i != 0)
            {
                Console.WriteLine();
            }

            for (int j = 0; j < speelBord.GetLength(1); j++)
            {
                Console.Write(speelBord[i, j]);
            }
        }
    }
    /// <summary>
    /// Maakt boterkaas en eieren tenzij je een string met waarde "Schuif" meegeeft 
    /// </summary>
    /// <param name="gameSelected">Als hier Schuif meegeeft dan krijg je een pre-generated bord die waarbij je zelf de X goed moet zetten</param>
    public static void BoterKaas(string gameSelected = "")
    {
        Console.CursorVisible = false;
        int pos1 = 0;
        int pos2 = 0;
    TryUntilPossible:
        char[,] speelBord =
        {
        { 'S', '□', '□' },
        { '□', '□', '□' },
        { '□', '□', '□' }
        };

        if (gameSelected == "Schuif")
        {
            int checkSolvable = 0;
            for (int i = 0; i < 3; i++)
            {
                int choosePos1 = rand.Next(0,3);
                int choosePos2 = rand.Next(0,3);
                if (speelBord[choosePos1, choosePos2] != 'S')
                {
                    speelBord[choosePos1, choosePos2] = 'X';
                }
                else { goto TryUntilPossible; }
            }
            for (int i = 0; i < speelBord.GetLength(0); i++)
            {
                for (int j = 0; j < speelBord.GetLength(1); j++)
                {
                    if (speelBord[i,j] == 'X')
                    { 
                       checkSolvable++;
                    }
                }
            }

            if (checkSolvable < 3 || HasPlayerWon('X', speelBord))
            {
                goto TryUntilPossible;
            }
            //deze twee checks zorgen dat er niet een bord met minder dan 3 X of een al opgelost bord
            //gemaakt is door elke keer opnieuw een bord
            //te maken totdat er een oplosbare is
        }

        for (int i = 0; i < speelBord.GetLength(0); i++)
        {
            for (int j = 0; j < speelBord.GetLength(1); j++)
            {
                while (true)
                {
                    Vector2 oldPosition = new Vector2(pos1, pos2);
                    PrintBord(speelBord);
                    var ButtonPressed = Console.ReadKey().Key;

                    //links en naar rechts
                    if (ButtonPressed == ConsoleKey.RightArrow && pos2 < speelBord.GetLength(0) - 1)
                    {
                        pos2++;
                    }
                    if (ButtonPressed == ConsoleKey.LeftArrow && pos2 > 0)
                    {
                        pos2--;
                    }

                    //omhoog en naarbeneden
                    if (ButtonPressed == ConsoleKey.UpArrow && pos1 > 0)
                    {
                        pos1--;
                    }
                    if (ButtonPressed == ConsoleKey.DownArrow && pos1 < speelBord.GetLength(1) - 1)
                    {
                        pos1++;
                    }

                    if (gameSelected != "Schuif")
                    { 
                      if (ButtonPressed == ConsoleKey.Enter)
                      {
                        if (speelBord[pos1, pos2] != 'X' && speelBord[pos1,pos2] != 'Y')
                        { 
                          break;
                        }
                      }  
                    }

                    ClearBoard(pos1, pos2, speelBord);

                    char oldPositionVal = speelBord[pos1, pos2];

                    speelBord[pos1, pos2] = 'S';
                    if (speelBord[pos1, pos2] == 'S' && oldPositionVal == 'X')
                    {
                        if (gameSelected == "Schuif")
                        {
                            speelBord[(int)oldPosition.X, (int)oldPosition.Y] = 'X';
                        }
                        else
                        {
                            speelBord[pos1, pos2] = 'X';
                        }
                    }
                    if (speelBord[pos1, pos2] == 'S' && oldPositionVal == 'Y')
                    {
                        if (gameSelected == "Schuif")
                        {
                            speelBord[(int)oldPosition.X, (int)oldPosition.Y] = 'Y';
                        }
                        else
                        {
                            speelBord[pos1, pos2] = 'Y';
                        }
                    }
                    PrintBord(speelBord);
                    Console.Write($" {pos1},{pos2}");
                    if (HasPlayerWon('X', speelBord))
                    {
                        Console.WriteLine("Je hebt gewonnen!");
                        Thread.Sleep(2000);
                        i = 4;
                        j = 4;
                        break;
                    }

                }
                speelBord[pos1, pos2] = 'X';
                PrintBord(speelBord);

                //Check wincondities vvv
            if (gameSelected != "Schuif")
            { 
                if (HasPlayerWon('X', speelBord))
                {
                    Console.WriteLine("Je hebt gewonnen!");
                    Thread.Sleep(2000);
                    i = 4;
                    j = 4;
                    break;
                }
                else if (FullBoard(speelBord))
                {
                    Console.WriteLine("Bord is vol");
                    Thread.Sleep(2000);
                    i = 4;
                    j = 4;
                    break;
                }


                if (HasPlayerWon('Y', speelBord))
                {
                    Console.WriteLine("Je hebt Verloren");
                    Thread.Sleep(2000);
                    i = 4;
                    j = 4;
                    break;
                }
                else if (FullBoard(speelBord))
                {
                    Console.WriteLine("Bord is vol");
                    Thread.Sleep(2000);
                    i = 4;
                    j = 4;
                    break;
                }
                // CheckWincondities ^^^
            }


                if (gameSelected != "Schuif")
                { 
                TryPlacingAgain:
                    int place1 = rand.Next(0,3);
                    int place2 = rand.Next(0,3);
               
                    if (speelBord[place1, place2] == 'X' || speelBord[place1, place2] == 'Y')
                    {
                       goto TryPlacingAgain;
                    }
                    else
                    { 
                       speelBord[place1,place2] = 'Y';
                    }
                }
            }
        }
    }
    /// <summary>
    /// Reset alle elementen in het bord behalve het element op spelbord[pos1,pos2]
    /// </summary>
    /// <param name="pos1">Eerste geselecteerde coordinaat</param>
    /// <param name="pos2">Tweede geselecteerde coordinaat</param>
    /// <param name="spelbord">2d char array</param>
    static void ClearBoard(int pos1, int pos2, char[,] spelbord)
    {
        for (int i = 0; i < spelbord.GetLength(0); i++)
        {
            for (int j = 0; j < spelbord.GetLength(1); j++)
            {
                if (spelbord[i, j] != spelbord[pos1, pos2] && spelbord[i, j] != 'X' && spelbord[i, j] != 'Y')
                {
                    spelbord[i, j] = '?';
                }
            }
        }
    }
    static bool FullBoard(char[,] spelbord)
    {
        int counter = 0;
        for (int i = 0; i < spelbord.GetLength(0); i++)
        {
            for (int j = 0; j < spelbord.GetLength(1); j++)
            {
                if (spelbord[i, j] != '?')
                {
                    if (spelbord[i, j] != 'S')
                    { 
                      counter++;  
                    }
                }
            }
        }
        if (counter == 9)
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }
    // Het was dit of 16 if statements om te checken wie gewonnen heef. het is half 6 in de ochtend
    static bool CheckHorizontalWin(char player , char[,] _speelBord)
    {
        for (int row = 0; row < 3; row++)
        {
            if (_speelBord[row, 0] == player && _speelBord[row, 1] == player && _speelBord[row, 2] == player)
            {
                return true;
            }
        }
        return false;
    }
    static bool CheckVerticalWin(char player, char[,] _speelBord)
    {
        for (int col = 0; col < 3; col++)
        {
            if (_speelBord[0, col] == player && _speelBord[1, col] == player && _speelBord[2, col] == player)
            {
                return true;
            }
        }
        return false;
    }
    static bool CheckDiagonalWin(char player, char[,] _speelBord)
    {
        if ((_speelBord[0, 0] == player && _speelBord[1, 1] == player && _speelBord[2, 2] == player) ||
            (_speelBord[0, 2] == player && _speelBord[1, 1] == player && _speelBord[2, 0] == player))
        {
            return true;
        }
        return false;
    }

    static bool HasPlayerWon(char player, char[,] _speelBord)
    {
        return CheckHorizontalWin(player, _speelBord) || CheckVerticalWin(player, _speelBord) || CheckDiagonalWin(player, _speelBord);
    }

}





