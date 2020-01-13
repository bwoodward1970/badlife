using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public interface IGameRules
    {
        int ApplyLiveRules(int state, int neighbours);
    }
}
