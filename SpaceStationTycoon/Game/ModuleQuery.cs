using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Game
{
    public interface IModuleQuery<T> where T : IModule
    {
        bool Check(T module);
        List<T> FindAllMatches(Station station);
        T FindFirstMatch(Station sation);
    }
}
