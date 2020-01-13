using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    /// <summary>
    /// Base Parser array convert methods
    /// </summary>
    public abstract class Parser
    {
        /// <summary>
        /// Convert one array using mapper func
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="fromValues"></param>
        /// <param name="setterFunc"></param>
        /// <returns></returns>
        protected T1[][] ConvertArrayValues<T, T1>(T[][] fromValues, Func<T, T1> setterFunc)
        {
            var result = new T1[fromValues.Length][];

            for (int i = 0; i < fromValues.Length; i++)
            {
                result[i] = new T1[fromValues[i].Length];

                AssignArrayValues(i, fromValues[i], result, setterFunc);
            }

            return result;
        }

        protected void AssignArrayValues<T, T1>(int row, T[] fromValues, T1[][] toValues, Func<T, T1> setterFunc)
        {
            for (int i = 0; i < fromValues.Length; i++)
            {
                toValues[row][i] = setterFunc(fromValues[i]);
            }
        }

    }
}
