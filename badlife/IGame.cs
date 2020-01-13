using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public interface IGame
    {
        void Initialize(char[][] displayValues);

        void Evolve();

        int[][] Values
        { get; }
    }
}
