using System;
using System.Collections.Generic;

namespace Sokoban_Imperative
{
    public class SokobanSolver
    {
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
                TileType[,] importPuzzle = SokobanReader.FromFile(filePath);
                
                if (importPuzzle == null)
                {
                    Console.WriteLine("Failed to import puzzle from the specified file.");
                    return;
                }
                
                SokobanPuzzle puzzle = new SokobanPuzzle(importPuzzle);
                            
                // Just for testing purposes
                // Console.WriteLine("ToString test:");
                // Console.WriteLine(puzzle.ToString());
                            
                bool solved = SolvePuzzle(puzzle);
                
                if (solved)
                {
                    Console.WriteLine("Puzzle solved: ");
                    Console.WriteLine(puzzle.ToString());
                }
                else
                {
                    Console.WriteLine("No solution found.");
                }
            }
        }

        public static bool SolvePuzzle(SokobanPuzzle startState)
        {
            Stack<SokobanPuzzle> stack = new Stack<SokobanPuzzle>();
            HashSet<string> visited = new HashSet<string>();
            
            stack.Push(startState);
            visited.Add(startState.ToString());

            while (stack.Count > 0)
            {
                SokobanPuzzle current = stack.Peek();
                Console.WriteLine(current.ToString());
                
                if (current.IsSolved())
                {
                    return true;
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
            
            return false;
        }
    }
}