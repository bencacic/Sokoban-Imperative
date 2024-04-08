using System;
using System.Collections.Generic;
using System.IO;

namespace Sokoban_Imperative
{
    /// <summary>
    /// Class <c>SokobanTester</c> The tester for our Sokoban puzzles.
    ///     Everything in the testPuzzles directory will be tested.
    /// </summary>
    public static class SokobanTester
    {
        /// <summary>
        /// Method <c>Test</c>
        ///     Runs a series of test puzzles located in the specified folder path.
        /// </summary>
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
            }
        }
    }
}