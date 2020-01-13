using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public interface IBoard
    {
        int this[int row, int col]
        { get; set; }

        int Rows
        { get; }

        int Cols
        { get; }

        int[][] Values
        { get; }

        int GetLiveNeighbours(int row, int col);

    }
}
