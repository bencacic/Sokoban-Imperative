using System;
using System.Collections.Generic;

namespace Sokoban_Imperative
{
    /*
     * Handles printing out the puzzle solution
     */
    public static class SokobanPrint
    {
     
        /*
         * Prints the puzzle solution.
         *
         * Parameters:
         *  StartState: the initial state the puzzle started from.
         *  SolutionStack: the stack containing each step of the puzzle solution.
         */
       // public static void PrintSolution(SokobanPuzzle startState, Stack<SokobanPuzzle> solutionStack)
       //  {
       //      Stack<SokobanPuzzle> tempStack = new Stack<SokobanPuzzle>();
       //      
       //      SokobanPuzzle current = solutionStack.Pop();
       //      
       //      while (true)
       //      {
       //          tempStack.Push(current);
       //          if (current.Equals(startState))
       //          {
       //              break;
       //          }
       //
       //          current = solutionStack.Pop();
       //      }
       //
       //      Console.WriteLine("Moves taken to solve the puzzle:");
       //  
       //      while (tempStack.Count > 0)
       //      {
       //          current = tempStack.Pop();
       //      
       //          Console.WriteLine(current.ToString());
       //      }
       //  }
       
       public static void PrintSolution(SokobanPuzzle startState, Stack<SokobanPuzzle> solutionStack)
{
    Stack<SokobanPuzzle> tempStack = new Stack<SokobanPuzzle>();

    SokobanPuzzle current = solutionStack.Pop();

    while (true)
    {
        tempStack.Push(current);
        if (current.Equals(startState))
        {
            break;
        }

        current = solutionStack.Pop();
    }

    Console.WriteLine("Moves taken to solve the puzzle:");

    SokobanPuzzle prev = null;
    while (tempStack.Count > 0)
    {
        current = tempStack.Pop();
        
        // Determine the direction of movement
        if (prev != null)
        {
            //Console.WriteLine();
            
            Console.WriteLine(GetDirection(prev, current));
            
        }
        else 
        {
            Console.WriteLine(GetDirection(null, current));
           // Console.WriteLine("*None*");
        }
        
        
        //Console.WriteLine(current.ToString());
        // Print the puzzle state without the additional rows and columns
        PrintPuzzleState(current);

        prev = current;
    }
}

public static void PrintPuzzleState(SokobanPuzzle puzzle)
{
    TileType[,] state = puzzle.State;
    int rows = state.GetLength(0);
    int cols = state.GetLength(1);

    for (int i = 1; i < rows - 1; i++)
    {
        for (int j = 1; j < cols - 1; j++)
        {
            switch (state[i, j])
            {
                case TileType.Wall:
                    Console.Write('X');
                    break;
                case TileType.Empty:
                    Console.Write(' ');
                    break;
                case TileType.Player:
                    Console.Write('P');
                    break;
                case TileType.PlayerGoal:
                    Console.Write('O');
                    break;
                case TileType.Box:
                    Console.Write('B');
                    break;
                case TileType.BoxGoal:
                    Console.Write('H');
                    break;
                case TileType.Goal:
                    Console.Write('G');
                    break;
            }
        }

        Console.WriteLine();
    }
    Console.WriteLine();
}

private static string GetDirection(SokobanPuzzle prev, SokobanPuzzle current)
{
    if (prev != null)
    {


        int playerRowPrev = GetPlayerRow(prev);
        int playerColPrev = GetPlayerCol(prev);

        int playerRowCurr = GetPlayerRow(current);
        int playerColCurr = GetPlayerCol(current);

        if (playerRowPrev == playerRowCurr)
        {
            if (playerColPrev < playerColCurr)
            {
                return "*Right*";
            }
            if (playerColPrev > playerColCurr)
            {
                return "*Left*";
            }
        }
        else if (playerColPrev == playerColCurr)
        {
            if (playerRowPrev < playerRowCurr)
            {
                return "*Down*";
            }
            if (playerRowPrev > playerRowCurr)
            {
                return "*Up*";
            }
        }
    }

    return "*None*";
}

private static int GetPlayerRow(SokobanPuzzle puzzle)
{
    TileType[,] state = puzzle.State;
    for (int i = 0; i < state.GetLength(0); i++)
    {
        for (int j = 0; j < state.GetLength(1); j++)
        {
            if (state[i, j] == TileType.Player || state[i, j] == TileType.PlayerGoal)
            {
                return i;
            }
        }
    }
    return -1;
}

private static int GetPlayerCol(SokobanPuzzle puzzle)
{
    TileType[,] state = puzzle.State;
    for (int i = 0; i < state.GetLength(0); i++)
    {
        for (int j = 0; j < state.GetLength(1); j++)
        {
            if (state[i, j] == TileType.Player || state[i, j] == TileType.PlayerGoal)
            {
                return j;
            }
        }
    }
    return -1;
}


    }
}
