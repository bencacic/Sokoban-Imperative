using System;
using System.Collections.Generic;

namespace Sokoban_Imperative
{
    /// <summary>
    /// Class <c>SokobanPrint</c> Handles all output regarding the puzzle state and solution.
    /// </summary>
    public static class SokobanPrint
    {
        /// <summary>
        /// Method <c>PrintSolution</c> Prints the solution steps of the solved puzzle.
        /// </summary>
        /// <param name="startState">The initial state of the puzzle.</param>
        /// <param name="solutionStack">The steps the solver took to produce a solution.</param>
       public static void PrintSolution(SokobanPuzzle startState, Stack<SokobanPuzzle> solutionStack)
        {
            Stack<SokobanPuzzle> tempStack = new Stack<SokobanPuzzle>();

            SokobanPuzzle current = solutionStack.Pop();

            while (true)
            {
                tempStack.Push(current);
                if (current.Equals(startState))
                {
                    break;
                }
                current = solutionStack.Pop();
            }

            Console.WriteLine("Moves taken to solve the puzzle:");

            SokobanPuzzle prev = null;
            while (tempStack.Count > 0)
            {
                current = tempStack.Pop();
        
                // Determine the direction of movement
                if (prev != null)
                {
                    Console.WriteLine(GetDirection(prev, current));
                }
                else 
                {
                    Console.WriteLine(GetDirection(null, current));
                }

                // Print the puzzle state without the additional rows and columns
                PrintPuzzleState(current);

                prev = current; 
            }
        }
        
        /// <summary>
        /// Method <c>PrintPuzzleState</c> Prints the current state of the puzzle.
        /// </summary>
        /// <param name="puzzle">The state of the puzzle.</param>
        public static void PrintPuzzleState(SokobanPuzzle puzzle)
        {
            TileType[,] state = puzzle.State;
            int rows = state.GetLength(0);
            int cols = state.GetLength(1);

            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    switch (state[i, j])
                    {
                        case TileType.Wall:
                            Console.Write('X');
                            break;
                        case TileType.Empty:
                            Console.Write(' ');
                            break;
                        case TileType.Player:
                            Console.Write('P');
                            break;
                        case TileType.PlayerGoal:
                            Console.Write('O');
                            break;
                        case TileType.Box:
                            Console.Write('B');
                            break;
                        case TileType.BoxGoal:
                            Console.Write('H');
                            break;
                        case TileType.Goal:
                            Console.Write('G');
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Method <c>GetDirection</c>
        ///     Is called by PrintSolution to determine the direction
        ///     the player took in order to solve the puzzle. This is handled after a
        ///     solution is found in order to reduce the total number of processes (in
        ///     the case that the puzzle is unsolvable).
        /// </summary>
        /// <param name="prev">The previous state of the puzzle.</param>
        /// <param name="current">The current state of the puzzle.</param>
        /// <returns>
        ///     A string representing the direction that the player has moved between two
        ///     consecutive game states.
        /// </returns>
        private static string GetDirection(SokobanPuzzle prev, SokobanPuzzle current)
        {
            if (prev != null)
            {
                int playerRowPrev = GetPlayerRow(prev);
                int playerColPrev = GetPlayerCol(prev);

                int playerRowCurr = GetPlayerRow(current);
                int playerColCurr = GetPlayerCol(current);

                if (playerRowPrev == playerRowCurr)
                {
                    if (playerColPrev < playerColCurr)
                    {
                        return "*Right*";
                    }
                    if (playerColPrev > playerColCurr)
                    {
                        return "*Left*";
                    }
                }
                else if (playerColPrev == playerColCurr)
                {
                    if (playerRowPrev < playerRowCurr)
                    {
                        return "*Down*";
                    }
                    if (playerRowPrev > playerRowCurr)
                    {
                        return "*Up*";
                    }
                }
            }

            return "*None*";
        }
        /// <summary>
        /// Method <c>GetPlayerRow</c> Called by GetDirection to determine the X coordinate of the player.
        /// </summary>
        /// <param name="puzzle">The state of the puzzle.</param>
        /// <returns>
        ///     An int that contains the row that the player is on a particular state.
        /// </returns>
        private static int GetPlayerRow(SokobanPuzzle puzzle)
        {
            TileType[,] state = puzzle.State;
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == TileType.Player || state[i, j] == TileType.PlayerGoal)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Method <c>GetPlayerCol</c> Called by GetDirection to determine the Y coordinate of the player.
        /// </summary>
        /// <param name="puzzle">The state of the puzzle.</param>
        /// <returns>
        ///     An int that contains the column that the player is on a particular state.
        /// </returns>
        private static int GetPlayerCol(SokobanPuzzle puzzle)
        {
            TileType[,] state = puzzle.State;
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == TileType.Player || state[i, j] == TileType.PlayerGoal)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }
    }
}
