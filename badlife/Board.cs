using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    /// <summary>
    /// Board Matrix to store game values
    /// </summary>
    public class Board : IBoard
    {
        private int[][] _world;

        /// <summary>
        /// Ctor initialising wi ull values
        /// </summary>
        /// <param name="parsedValues"></param>
        public Board(int[][] parsedValues)
        {
            _world = parsedValues;
        }

        /// <summary>
        /// Ctor for an empty board
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public Board(int rows, int cols)
        {
            _world = new int[rows][];

            for(int i = 0; i < rows; i++)
            {
                _world[i] = new int[cols];
            }
        }
       
        /// <summary>
        /// Indexer, gets/sets a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int this[int row, int col]
        {
            get { return _world[row][col]; }
            set { _world[row][col] = value; }
        }

        public int Rows => _world.Length;

        public int Cols => _world[0].Length;

        public int[][] Values => _world;
        
        /// <summary>
        /// Get live neighbours
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int GetLiveNeighbours(int row, int col) =>
            RowLiveCount(row, _world.Length, col, PreviousPosition)
                + RowLiveCount(row, col) + (_world[row][col] * -1)
                + RowLiveCount(row, _world.Length, col, NextPosition);

        /// <summary>
        /// Get Row live/dead total using row offset
        /// </summary>
        /// <param name="row"></param>
        /// <param name="max"></param>
        /// <param name="col"></param>
        /// <param name="rowOffSetter"></param>
        /// <returns></returns>
        protected int RowLiveCount(int row, int max, int col, Func<int,int, int> rowOffSetter) =>
                RowLiveCount(rowOffSetter(row, max), col);
        
        /// <summary>
        /// Get Row Neighbours live/dead count
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        protected virtual int RowLiveCount(int row, int col) => CountLive(row, col, PreviousPosition)
                + _world[row][col] + CountLive(row, col, NextPosition);

        /// <summary>
        /// Get Cell live/dead Value using Column offset
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="colOffSetter"></param>
        /// <returns></returns>
        protected virtual int CountLive(int row, int col, Func<int, int, int> colOffSetter) =>
                _world[row][colOffSetter(col, _world[0].Length)];
                
        /// <summary>
        /// Get the Previous Row/Col Number
        /// </summary>
        /// <param name="position"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        protected int PreviousPosition(int position, int max) => position - 1 < 0 ? max - 1 : position - 1;

        /// <summary>
        /// Get the Next Row/Col Number
        /// </summary>
        /// <param name="position"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        protected int NextPosition(int position, int max) => position + 1 > (max - 1) ? 0 : position + 1;
        
    }
}
