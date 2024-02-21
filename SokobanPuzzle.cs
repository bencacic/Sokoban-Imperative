using System.Collections.Generic;
using System.Configuration;
using System.Security;

namespace Sokoban_Imperative
{
    enum TileType
    {
        Wall,
        Empty,
        Player,
        PlayerGoal,
        Box,
        BoxGoal,
        Goal
    }
    
    public class SokobanPuzzle
    {
        const int Up = 0;
        const int Down = 1;
        const int Right = 2;
        const int Left = 3;
        private TileType[,] state;
        
        //this constructor is meant to build from an existing puzzle object, hence the error, so
        //TODO - build a proper constructor
        public SokobanPuzzle(TileType[,] initialState)
        {
            state = (TileType[,])initialState.Clone();
        }

        public bool IsSolved()
        {
            foreach (var tile in state)
            {
                if (tile == TileType.Box)
                    return false;
            }
            return true;
        }
        
        public IEnumerable<SokobanPuzzle> GetPossibleMoves()
        {
            List<SokobanPuzzle> possibleMoves = new List<SokobanPuzzle>();
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == TileType.Player)
                    {
                        if (IsValidMove(i - 1, j, Up))
                        {
                            SokobanPuzzle moveUp = MovePLayer(i, j, i - 1, j);
                            possibleMoves.Add(moveUp);
                        } 
                        if (IsValidMove(i + 1, j, Down))
                        {
                            SokobanPuzzle moveDown = MovePLayer(i, j, i + 1, j);
                            possibleMoves.Add(moveDown);
                        }
                        if (IsValidMove(i, j - 1, Right))
                        {
                            SokobanPuzzle moveRight = MovePLayer(i, j, i, j - 1);
                            possibleMoves.Add(moveRight);
                        }

                        if (IsValidMove(i, j + 1, Left))
                        {
                            SokobanPuzzle moveLeft = MovePLayer(i, j, i, j + 1);
                            possibleMoves.Add(moveLeft);
                        }

                        break;
                    }
                }
            }

            return possibleMoves;
        }

        private bool IsValidMove(int row, int col, int direction)
        {
            if (row < 0 || row >= state.GetLength(0) || col < 0 || col >= state.GetLength(1))
                return false;

            if (state[row, col] != TileType.Wall)
            {
                if (state[row, col] == TileType.Box || state[row, col] == TileType.BoxGoal)
                {
                    int behindRow = row, behindCol = col;
                    switch (direction)
                    {
                        case Up:
                            behindRow += 1;
                            break;
                        case Down:
                            behindRow -= 1;
                            break;
                        case Right:
                            behindCol -= 1;
                            break;
                        case Left:
                            behindCol += 1;
                            break;
                    }

                    if (state[behindRow, behindCol] == TileType.Wall || state[behindRow, behindCol] == TileType.Box || 
                        state[behindRow, behindCol] == TileType.BoxGoal)
                        return false;
                }

                return true;
            }

            return false;
        }

        private SokobanPuzzle MovePLayer(int fromRow, int fromCol, int toRow, int toCol)
        {
            TileType[,] newState = (TileType[,])state.Clone();
            
            // Move the player to the destination space
            newState[toRow, toCol] = newState[fromRow, fromCol] == TileType.PlayerGoal ? 
                TileType.PlayerGoal : TileType.Player;

            // Update the source space
            newState[fromRow, fromCol] = newState[fromRow, fromCol] == TileType.PlayerGoal ? 
                TileType.Goal : TileType.Empty;

            // If the destination space contains a box or a box on a goal, move the box accordingly
            if (newState[toRow, toCol] == TileType.Box || newState[toRow, toCol] == TileType.BoxGoal)
            {
                int boxToRow = toRow + (toRow - fromRow);
                int boxToCol = toCol + (toCol - fromCol);
                newState[boxToRow, boxToCol] = newState[toRow, toCol] == TileType.BoxGoal ? 
                    TileType.Goal : TileType.Box;
            }
            
            return new SokobanPuzzle(newState);
        }

        //TODO - convert a puzzle to a string, which will work with a constructor here to rebuild it into an object.
        public override string ToString()
        {
            return "";
        }
    }
}