using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    /// <summary>
    /// Parser to convert Game board array to display characters
    /// and back again
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public class DisplayParser<T, T1> : Parser, IDisplayParser<T, T1>
    {
        private readonly Func<T1, T> _toMapper;
        private readonly Func<T, T1> _fromMapper;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mappers"></param>
        private DisplayParser((Func<T1, T>, Func<T, T1>) mappers)
        {
            _toMapper = mappers.Item1;
            _fromMapper = mappers.Item2;
        }

        /// <summary>
        /// Create Method
        /// </summary>
        /// <param name="mappers"></param>
        /// <returns></returns>
        public static DisplayParser<T, T1> Create((Func<T1, T>, Func<T, T1>) mappers) 
            => new DisplayParser<T, T1>(mappers);

        /// <summary>
        /// Create Board Array from Display Grid
        /// </summary>
        /// <param name="gridValues"></param>
        /// <returns></returns>
        public T[][] ParseDisplayGrid(T1[][] gridValues) => CreateGridRows(gridValues);

        /// <summary>
        /// Convert display values to board grid
        /// </summary>
        /// <param name="displayValues"></param>
        /// <returns></returns>
        protected T[][] CreateGridRows(T1[][] displayValues) => ConvertArrayValues(displayValues, GetBoardValue);
        
        /// <summary>
        /// Map Display to Board Value
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        protected T GetBoardValue(T1 c) => _toMapper(c);

        /// <summary>
        /// Map Board to Display Value
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected T1 GetDisplayValue(T state) => _fromMapper(state);

        /// <summary>
        /// Convert Board to Display Values
        /// </summary>
        /// <param name="gridValues"></param>
        /// <returns></returns>
        public T1[][] GetDisplayValues(T[][] gridValues) => ConvertArrayValues(gridValues, GetDisplayValue);


       
    }
}
