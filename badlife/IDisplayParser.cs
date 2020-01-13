using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    /// <summary>
    /// Display Parser Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public interface IDisplayParser<T, T1>
    {
        T[][] ParseDisplayGrid(T1[][] gridValues);

        T1[][] GetDisplayValues(T[][] gridValues);

    }
}
