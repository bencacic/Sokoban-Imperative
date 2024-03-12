using System;
using System.Collections.Generic;
using System.Text;


namespace Sokoban_Imperative
{
    public enum TileType
    {
        Wall,
        Empty,
        Player,
        PlayerGoal,
        Box,
        BoxGoal,
        Goal
    }
    
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    public class SokobanPuzzle
    {
        private readonly TileType[,] _state;
        
        // This constructor copies an existing puzzle object, so that the game state can be modified.
        public SokobanPuzzle(TileType[,] initialState)
        {
            _state = (TileType[,])initialState.Clone();
        }

        public bool IsSolved()
        {
            foreach (var tile in _state)
            {
                if (tile == TileType.Goal || tile == TileType.PlayerGoal)
                    return false;
            }
            return true;
        }
        
        public IEnumerable<SokobanPuzzle> GetPossibleMoves()
        {
            List<SokobanPuzzle> possibleMoves = new List<SokobanPuzzle>();
            int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } }; // Up, Down, Left, Right
            
            for (int i = 0; i < _state.GetLength(0); i++)
            {
                for (int j = 0; j < _state.GetLength(1); j++)
                {
                    if (_state[i, j] == TileType.Player)
                    {
                        for (int k = 0; k < directions.GetLength(0); k++)
                        {
                            int newX = i + directions[k, 0];
                            int newY = j + directions[k, 1];

                            if (IsValidMove(newX, newY, (Direction)k))
                            {
                                SokobanPuzzle newMove = MovePLayer(i, j, newX, newY);
                                possibleMoves.Add(newMove);
                            }
                        }

                        break;
                    }
                }
            }

            return possibleMoves;
        }

        private bool IsValidMove(int row, int col, Direction direction)
        {
            if (row < 0 || row >= _state.GetLength(0) || col < 0 || col >= _state.GetLength(1))
                return false;

            TileType currentTile = _state[row, col];

            if (currentTile == TileType.Wall)
                return false;

            if (currentTile == TileType.Box || currentTile == TileType.BoxGoal)
            {
                int behindRow = row + (direction == Direction.Down ? 1 : direction == Direction.Up ? -1 : 0);
                int behindCol = col + (direction == Direction.Right ? -1 : direction == Direction.Left ? 1 : 0);

                TileType behindTile = _state[behindRow, behindCol];

                if (behindTile == TileType.Wall || behindTile == TileType.Box || behindTile == TileType.BoxGoal)
                    return false;
            }

            return true;
        }

        private SokobanPuzzle MovePLayer(int fromRow, int fromCol, int toRow, int toCol)
        {
            TileType[,] newState = (TileType[,])_state.Clone();
            
            bool isGoalSpace = newState[toRow, toCol] == TileType.Goal || newState[toRow, toCol] == TileType.BoxGoal;
            
            // Move the player to the destination space
            newState[toRow, toCol] = isGoalSpace ? TileType.PlayerGoal : TileType.Player;

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

        // Converts a puzzle object into a string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _state.GetLength(0); i++)
            {
                for (int j = 0; j < _state.GetLength(1); j++)
                {
                    switch (_state[i, j])
                    {
                        case TileType.Wall:
                            sb.Append('X');
                            break;
                        case TileType.Empty:
                            sb.Append(' ');
                            break;
                        case TileType.Player:
                            sb.Append('O');
                            break;
                        case TileType.PlayerGoal:
                            sb.Append('P');
                            break;
                        case TileType.Box:
                            sb.Append('B');
                            break;
                        case TileType.BoxGoal:
                            sb.Append('G');
                            break;
                        case TileType.Goal:
                            sb.Append('H');
                            break;
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}