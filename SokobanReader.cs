using System;
using System.IO;
using System.Linq;

namespace Sokoban_Imperative
{
    public static class SokobanReader
    {
        public static TileType[,] FromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Length;
            
            int colsMax = lines.Max(line => line.Length);
            
            TileType[,] puzzle = new TileType[rows, colsMax];

            for (int i = 0; i < rows; i++)
            {
                string trimmedLine = lines[i].TrimEnd();

                for (int j = 0; j < trimmedLine.Length; j++)
                {
                    char symbol = trimmedLine[j];
                    puzzle[i, j] = InitTile(symbol);
                }
            }
            
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
                    throw new ArgumentException("Invalid character in the puzzle");
            }
        }
    }
}
    


