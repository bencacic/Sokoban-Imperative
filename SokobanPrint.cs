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
        
            while (tempStack.Count > 0)
            {
                current = tempStack.Pop();
            
                Console.WriteLine(current.ToString());
            }
        }

    }
}
