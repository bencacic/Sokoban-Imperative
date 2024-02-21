using System;
using System.Collections.Generic;
using System.IO;

namespace Sokoban_Imperative
{
    public class SokobanSolver
    {
        static void Main(string[] args)
        {
            TileType[,] importPuzzle; //placeholder stuff here, swap out for whatever we use for file input

            SokobanPuzzle puzzle = new SokobanPuzzle(importPuzzle);

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

        static bool SolvePuzzle(SokobanPuzzle startState)
        {
            Queue<SokobanPuzzle> queue = new Queue<SokobanPuzzle>();
            HashSet<string> visited = new HashSet<string>();
            
            queue.Enqueue(startState);
            visited.Add(startState.ToString());

            while (queue.Count > 0)
            {
                SokobanPuzzle current = queue.Dequeue();

                if (current.IsSolved())
                {
                    return true;
                }

                foreach (SokobanPuzzle next in current.GetPossibleMoves())
                {
                    string nextState = next.ToString();
                    if (!visited.Contains(nextState))
                    {
                        queue.Enqueue(next);
                        visited.Add(nextState);
                    }
                }
            }

            return false;
        }
    }
}