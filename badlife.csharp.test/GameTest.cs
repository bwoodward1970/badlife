using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using Moq;

namespace badlife.csharp.test
{
    public class GameTest
    {
        private char[][] _lines;
        private IDisplayParser<int, char> _displayParser;
        private IBoardFactory _boardFactory;

        public GameTest()
        {
            _displayParser = DisplayParser<int, char>
                .Create(((c) => c == '*' ? 1 : 0, (state) => state == 1 ? '*' : '_'));

            var lines = new[] { "_____", "__*__", "_***_", "__*__", "_____" };
            _lines = lines.Select(s => s.ToCharArray()).ToArray();

            var boardFactoryMock = new Mock<IBoardFactory>();

            var board = new Board(_displayParser.ParseDisplayGrid(_lines));

            boardFactoryMock.Setup(bf => bf.Create(It.IsAny<char[][]>())).Returns(board);
            boardFactoryMock.Setup(bf => bf.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(board);

            _boardFactory = boardFactoryMock.Object;

        }

        [Theory]
        [InlineData(new[] { "_____", "_***_", "_*_*_", "_***_", "_____" }, 1)]
        [InlineData(new[] { "__*__", "_*_*_", "*___*", "_*_*_", "__*__" }, 2)]
        [InlineData(new[] { "_***_", "*****", "**_**", "*****", "_***_" }, 3)]
        [InlineData(new[] { "_____", "_____", "_____", "_____", "_____" }, 4)]
        public void GameEvolveTest(string[] expected, int iterations)
        {
            var game = new Game(new GameRules(), _boardFactory);

            game.Initialize(_lines);

            for (int i = 0; i < iterations; i++)
            {
                game.Evolve();
            }

            var values = _displayParser.GetDisplayValues(game.Values);

            string[] result = values.Select(ca => new string(ca)).ToArray();


            Assert.Equal(expected, result);

        }

    }
}
