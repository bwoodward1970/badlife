﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public interface IBoardFactory
    {
        IBoard Create(char[][] displayValues);

        IBoard Create(int rows, int cols);
    }
}
