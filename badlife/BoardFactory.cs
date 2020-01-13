using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

namespace badlife
{
    /// <summary>
    /// Create a Game Board in different states
    /// </summary>
    internal class BoardFactory : IBoardFactory
    {
        private readonly IDisplayParser<int, char> _parser;
        private readonly IComponentContext _componentContext;

        public BoardFactory(IDisplayParser<int, char> parser, IComponentContext componentContext)
        {
            _parser = parser;
            _componentContext = componentContext;
        }

        /// <summary>
        /// Create Game Board from display input
        /// </summary>
        /// <param name="displayValues"></param>
        /// <returns></returns>
        public IBoard Create(char[][] displayValues)
        {
            return _componentContext
                .Resolve<Board>(new TypedParameter(typeof(int[][]), 
                _parser.ParseDisplayGrid(displayValues)));
        }

        /// <summary>
        /// Create Empty Game board
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public IBoard Create(int rows, int cols)
        {
            return _componentContext
                .Resolve<Board>(new TypedParameter(typeof(int), rows),
                new TypedParameter(typeof(int), cols));
        }
    }
}
