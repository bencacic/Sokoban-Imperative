using System;
using System.IO;
using System.Linq;

namespace Sokoban_Imperative
{
    public class SokobanReader
    {
        public static TileType[,] FromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length - 1;
            int cols = lines[0].Length;
            
            // Just for testing purposes
            // Console.WriteLine("File Content:");
            // foreach (string line in lines)
            // {
            //     string trimmedLine = line.Trim();
            //     Console.WriteLine(trimmedLine);
            // }
            
            int colsMax = lines.Max(line => line.Length);
            
            TileType[,] puzzle = new TileType[rows, colsMax];

            for (int i = 0; i < rows; i++)
            {
                string trimmedLine = lines[i].Trim();

                for (int j = 0; j < trimmedLine.Length; j++)
                {
                    char symbol = trimmedLine[j];
                    puzzle[i, j] = InitTile(symbol);
                }
            }

            // Just for testing purposes (makes sure that the puzzle object getting initialized)
            // Console.WriteLine("Object to string test: ");
            // for (int i = 0; i < rows; i++)
            // {
            //     for (int j = 0; j < colsMax; j++)
            //     {
            //         Console.WriteLine(puzzle[i, j]);
            //     }
            // }
            
            return puzzle;
        }
        
        
        // Builds the initial puzzle object from the input
        private static TileType InitTile(char symbol)
        {
            switch (symbol)
            {
                case 'X':
                    return TileType.Wall;
                case ' ':
                    return TileType.Empty;
                case 'P':
                    return TileType.Player;
                case 'O':
                    return TileType.PlayerGoal;
                case 'B':
                    return TileType.Box;
                case 'H':
                    return TileType.BoxGoal;
                case 'G':
                    return TileType.Goal;
                default:
                    throw new ArgumentException("Invalid character in the puzzle.");
            }
        }
    }
}
    


