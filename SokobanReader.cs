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
            if (rows == 0)
            {
                throw new ArgumentException("File is empty");
            }
            int colsMax = lines.Max(line => line.Length) + 2;
            int playerCount = 0;
            int boxCount = 0;
            int goalCount = 0;
            
            TileType[,] puzzle = new TileType[rows + 2, colsMax];

            for (int i = 0; i < rows + 2; i++)
            {
                for (int j = 0; j < colsMax; j++)
                {
                    if (i == 0 || i == rows + 1 || j == 0 || j == colsMax - 1)
                    {
                        puzzle[i, j] = TileType.Wall;
                    }
                    else
                    {
                        puzzle[i, j] = TileType.Empty;
                    }
                }
            }

            for (int i = 0; i < rows; i++)
            {
                string trimmedLine = lines[i].TrimEnd();

                for (int j = 0; j < trimmedLine.Length; j++)
                {
                    char symbol = trimmedLine[j];
                    puzzle[i + 1, j + 1] = InitTile(symbol);
                        if (symbol == 'P')
                        {
                            playerCount++;
                        }
                        else if (symbol == 'B')
                        {
                            boxCount++;
                        }
                        else if (symbol == 'G')
                        {
                            goalCount++;
                        }
                        
                }
            }
            ErrorEventArgs( playerCount,  boxCount,  goalCount, rows);
            return puzzle;
        }

        private static void ErrorEventArgs(int players, int boxes, int goals, int rows)
        {
            
            if (players > 1)
            {
                throw new ArgumentException("More than one player in the puzzle");
            }
            if (players == 0 && goals > 0)
            {
                throw new ArgumentException("There is a goal without player in the puzzle");
            } 
            if (boxes < goals)
            {
                throw new ArgumentException("More goals than boxes in the puzzle");
                
            }
            
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
    


