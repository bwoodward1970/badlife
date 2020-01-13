using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    /// <summary>
    /// Game of Life Game Controller
    /// </summary>
    public class Game : IGame
    {
        private IBoard _gameBoard;
        private readonly IGameRules _gameRules;
        private readonly IBoardFactory _boardFactory;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="parser"></param>
        public Game(IGameRules gameRules, IBoardFactory boardFactory)
        {
            _gameRules = gameRules;
            _boardFactory = boardFactory;
        }
        
        /// <summary>
        /// Iniialize game and create the game board
        /// </summary>
        /// <param name="displayValues"></param>
        public void Initialize(char[][] displayValues)
        {
            _gameBoard = CreateBoard(displayValues);
        }

        /// <summary>
        /// Game board array
        /// </summary>
        public int[][] Values => _gameBoard.Values;

        /// <summary>
        /// Initialize and create game board
        /// </summary>
        /// <param name="displayValues"></param>
        /// <returns></returns>
        protected IBoard CreateBoard(char[][] displayValues) =>
            _boardFactory.Create(displayValues);

        /// <summary>
        /// Evolve game to next generation
        /// </summary>
        public void Evolve() => _gameBoard = Evolve(_gameBoard);
        
        /// <summary>
        /// Evolve game board to next generation
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        protected IBoard Evolve(IBoard board)
        {
            var newBoard = _boardFactory.Create(board.Rows, board.Cols);
               
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Cols; j++)
                {
                    newBoard[i, j] = GetNewState(board, i, j);
                }
            }

            return newBoard;
        }
        
        /// <summary>
        /// Apply the new Cell State
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        protected int GetNewState(IBoard board, int row, int col) => 
               _gameRules.ApplyLiveRules(board[row, col], board.GetLiveNeighbours(row, col));

        

    }
}
