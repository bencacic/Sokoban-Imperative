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
            Console.WriteLine(GetDirection(prev, current));
            
        }
        else 
        {
            Console.WriteLine(GetDirection(null, current));
           // Console.WriteLine("*None*");
        }
        

        Console.WriteLine(current.ToString());

        prev = current;
    }
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
