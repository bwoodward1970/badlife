using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

namespace badlife
{
    internal class BoardFactory : IBoardFactory
    {
        private readonly IDisplayParser<int, char> _parser;
        private readonly IComponentContext _componentContext;

        public BoardFactory(IDisplayParser<int, char> parser, IComponentContext componentContext)
        {
            _parser = parser;
            _componentContext = componentContext;
        }

        public IBoard Create(char[][] displayValues)
        {
            return _componentContext
                .Resolve<Board>(new TypedParameter(typeof(int[][]), 
                _parser.ParseDisplayGrid(displayValues)));
        }

        public IBoard Create(int rows, int cols)
        {
            return _componentContext
                .Resolve<Board>(new TypedParameter(typeof(int), rows),
                new TypedParameter(typeof(int), cols));
        }
    }
}
