using System;
using System.Collections.Generic;
using System.IO;

namespace Sokoban_Imperative
{
    public static class SokobanTester
    {
        public static void Test()
        {
            string folderPath = "../../testPuzzles";
            string[] files = Directory.GetFiles(folderPath);
            Array.Sort(files);
            foreach (string file in files)
            {
                String fileName = Path.GetFileName(file);
                try
                {
                    TileType[,] importPuzzle = SokobanReader.FromFile(file);
                    SokobanPuzzle puzzle = new SokobanPuzzle(importPuzzle);
                    Stack<SokobanPuzzle> solutionStack = SokobanSolver.SolvePuzzle(puzzle);
                    bool solved = solutionStack.Count > 0;
                    Console.WriteLine((solved ? "Puzzle solved: " : "Puzzle unsolvable: ") + fileName);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message + ": " + fileName);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Empty file: " + fileName);
                }
            }
        }
    }
}