using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public sealed class GameRules : IGameRules
    {

        /// <summary>
        /// Apply Game Live/Die Rules
        /// </summary>
        /// <remarks>
        ///  If a cell is ON and has fewer than two neighbors that are ON, it turns OFF
        ///  If a cell is ON and has either two or three neighbors that are ON, it remains ON.
        ///  If a cell is ON and has more than three neighbors that are ON, it turns OFF.
        ///  If a cell is OFF and has exactly three neighbors that are ON, it turns ON
        /// </remarks>
        /// <param name="state"></param>
        /// <param name="neighbours"></param>
        /// <returns></returns>
        public int ApplyLiveRules(int state, int neighbours)
        {            
            switch ((state, neighbours))
            {
                case var o when o.state == 1 && o.neighbours < 2:
                    return 0;
                case var o when o.state == 1 && (o.neighbours == 2 || o.neighbours == 3):
                    return 1;
                case var o when o.state == 1 && o.neighbours > 3:
                    return 0;
                case var o when o.state == 0 && o.neighbours == 3:
                    return 1;
                default:
                    return 0;

            }
        }
        
    }
}
