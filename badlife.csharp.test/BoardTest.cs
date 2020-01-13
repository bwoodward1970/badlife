using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace badlife.csharp.test
{
    public class BoardTest
    {
        private char[][] _lines;
        private IDisplayParser<int, char> _displayParser;

        public BoardTest()
        {
            var lines = new[] { "_____", "__*__", "_***_", "__*__", "_____" };

            _lines = lines.Select(s => s.ToCharArray()).ToArray();

            _displayParser = DisplayParser<int, char>
                .Create(((c) => c == '*' ? 1 : 0, (state) => state == 1 ? '*' : '_'));

        }

        [Fact]
        public void CreateBoardTest()
        {
            var board = new Board(_displayParser.ParseDisplayGrid(_lines));
            
            Assert.Equal(5, board[1, 2] + board[2, 1] + board[2, 2] + board[2, 3] + board[3, 2]);
        }

        [Fact]
        public void GetDisplayBoardTest()
        {
            var board = new Board(_displayParser.ParseDisplayGrid(_lines));

            var resultArray = _displayParser.GetDisplayValues(board.Values);

            Assert.Equal(_lines, resultArray);
        }

        [Theory]
        [InlineData(new[] { "_____", "__*__", "_***_", "__*__", "_____" }, 1, 2, 3)]
        [InlineData(new[] { "_____", "__*__", "_***_", "__*__", "_____" }, 2, 2, 4)]
        [InlineData(new[] { "_____", "__*__", "_***_", "__*__", "_____" }, 1, 3, 3)]
        [InlineData(new[] { "_____", "__*__", "_***_", "__*__", "_____" }, 1, 4, 1)]
        [InlineData(new[] { "_____", "_____", "_____", "_____", "_____" }, 2, 2, 0)]
        [InlineData(new[] { "_____", "*_*__", "_***_", "__*__", "_____" }, 1, 4, 2)]
        public void CountLiveTest(string[] lines, int row, int col, int expected)
        {
            var linesArray = lines.Select(s => s.ToCharArray()).ToArray();

            var board = new Board(_displayParser.ParseDisplayGrid(_lines));

            int liveCount = board.GetLiveNeighbours(row, col);

            Assert.Equal(expected, liveCount);

        }
        
       
    }
}
