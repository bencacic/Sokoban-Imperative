using System;
using System.IO;

namespace Sokoban_Imperative
{
    public class SokobanTester
    {
        public static void Test()
        {
            string folderPath = "../../testPuzzles";
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                String fileName = Path.GetFileName(file);
                TileType[,] importPuzzle = SokobanReader.FromFile(file);
                SokobanPuzzle puzzle = new SokobanPuzzle(importPuzzle);
                bool solved = SokobanSolver.SolvePuzzle(puzzle);
                Console.WriteLine((solved ? "Puzzle solved: " : "Puzzle unsolvable: ") + fileName);
            }
        }
    }
}