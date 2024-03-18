using System;
using System.Collections.Generic;
using System.IO;

namespace Sokoban_Imperative
{
    /*
    * The solver for our Sokoban puzzles.
    */
    public static class SokobanSolver
    {
        /*
        * Main of the SokobanSolver program.
        */
        static void Main(string[] args)
        {

            Console.WriteLine("Enter the filepath, or test:");
            string filePath = Console.ReadLine();
            
            if (filePath == "test")
            {
                SokobanTester.Test();
            }
            else
            {
                try
                {
                    TileType[,] importPuzzle = SokobanReader.FromFile(filePath);
                    if (importPuzzle == null)
                    {
                        Console.WriteLine("Failed to import puzzle from the specified file.");
                        return;
                    }

                    SokobanPuzzle puzzle = new SokobanPuzzle(importPuzzle);
                    Stack<SokobanPuzzle> solutionStack = SolvePuzzle(puzzle);

                    if (solutionStack.Count > 0)
                    {
                        SokobanPrint.PrintSolution(puzzle, solutionStack);
                        Console.WriteLine("Puzzle solved.");
                        
                    }
                    else
                    {
                        Console.WriteLine("No solution found.");
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found, or invalid filepath.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /*
        * Solves the Sokoban puzzle starting from the specified initial state.
        *
        * Parameters:
        *   startState: The initial state of the Sokoban puzzle.
        *
        * Returns:
        *   A stack of SokobanPuzzle instances representing the solution path, if found; otherwise, an empty stack.
        */
        public static Stack<SokobanPuzzle> SolvePuzzle(SokobanPuzzle startState)
        {
            Stack<SokobanPuzzle> stack = new Stack<SokobanPuzzle>();
            HashSet<string> visited = new HashSet<string>();
            
            stack.Push(startState);
            visited.Add(startState.ToString());

            while (stack.Count > 0)
            {
                SokobanPuzzle current = stack.Peek();
                //Console.WriteLine(current.ToString());
                
                if (current.IsSolved())
                {
                    return stack;
                }

                bool foundMove = false;

                foreach (SokobanPuzzle next in current.GetPossibleMoves())
                {
                    string nextState = next.ToString();
                    if (!visited.Contains(nextState))
                    {
                        stack.Push(next);
                        visited.Add(nextState);
                        foundMove = true;
                        break;
                    }
                }

                if (!foundMove)
                {
                    stack.Pop();
                }
            }
            
            return new Stack<SokobanPuzzle>();
        }
    }
}