using System.Collections.Generic;
using System.Configuration;

namespace Sokoban_Imperative
{
    enum TileType
    {
        Wall,
        Empty,
        Player,
        Box,
        BoxFinal,
        Goal
    }
    
    public class SokobanPuzzle
    {
        private TileType[,] state;
        
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
                        if (IsValidMove(i - 1, j))
                        {
                            SokobanPuzzle moveUp = MovePLayer(i, j, i - 1, j);
                            possibleMoves.Add(moveUp);
                        } 
                        if (IsValidMove(i + 1, j))
                        {
                            SokobanPuzzle moveDown = MovePLayer(i, j, i + 1, j);
                            possibleMoves.Add(moveDown);
                        }
                        if (IsValidMove(i, j - 1))
                        {
                            SokobanPuzzle moveRight = MovePLayer(i, j, i, j - 1);
                            possibleMoves.Add(moveRight);
                        }

                        if (IsValidMove(i, j + 1))
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

        private bool IsValidMove(int row, int col)
        {
            if (row < 0 || row >= state.GetLength(0) || col < 0 || col >= state.GetLength(1))
                return false;
            return state[row, col] != TileType.Wall;
        }

        private SokobanPuzzle MovePLayer(int fromRow, int fromCol, int toRow, int toCol)
        {
            TileType[,] newState = (TileType[,])state.Clone();
            newState[fromRow, fromCol] = TileType.Empty;
            newState[toRow, toCol] = TileType.Player;
            return new SokobanPuzzle(newState);
        }

        public override string ToString()
        {
            return "";
        }
    }
}