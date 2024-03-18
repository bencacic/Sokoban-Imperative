using System;
using System.IO;
using System.Linq;

namespace Sokoban_Imperative
{
    /*
     * Responsible for reading in our puzzle from a file.
     */
    public static class SokobanReader
    {
        /*
         * Reads the puzzle from a file.
         *
         * Parameters:
         *  filePath: The path to the file to be read.
         *
         * Returns:
         *  An object made up of specific tile types; the puzzle as understood by the rest of the program.
         */
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
        
        
        /*
         * Converts a character contained in the input file into its appropriate tile type.
         *
         * Parameters:
         *  symbol: The specific character symbol being looked at.
         *
         * Returns:
         *  The tile type of the given character.
         */
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
    


