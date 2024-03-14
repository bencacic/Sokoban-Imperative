using System.IO;

namespace Sokoban_Imperative
{
    public class SokobanTester
    {
        static void Test()
        {
            //want to go through a folder, and for each item in the folder, run that puzzle
            string folderPath = "../../testPuzzles";
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                TileType[,] importPuzzle = SokobanReader.FromFile(file);
                SokobanPuzzle puzzle = new SokobanPuzzle(importPuzzle);
                bool solved = SokobanSolver.SolvePuzzle(puzzle);
            }
        }
    }
}