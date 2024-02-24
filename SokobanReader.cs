using System;
using System.IO;

namespace Sokoban_Imperative
{
    public class SokobanReader
    {
        // Reads from .txt file to have puzzle initialized
        public static TileType[,] FromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Length;

            TileType[,] puzzle = new TileType[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    char symbol = lines[i][j];
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
                case 'O':
                    return TileType.Player;
                case 'P':
                    return TileType.PlayerGoal;
                case 'B':
                    return TileType.Box;
                case 'G':
                    return TileType.BoxGoal;
                default:
                    throw new ArgumentException("Invalid character in the puzzle.");
            }
        }
    }
}
    


