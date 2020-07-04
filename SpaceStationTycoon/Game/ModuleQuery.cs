using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Game
{
    public interface IModuleQuery<T> where T : class, IModule
    {
        bool Check(T module);
        List<T> FindAllMatches(Station station);
        T FindFirstMatch(Station sation);
    }

    public abstract class ModuleQuery<T> : IModuleQuery<T> where T : class, IModule
    {
        public abstract bool Check(T module);

        public virtual List<T> FindAllMatches(Station station) {
            List<T> modules = new List<T>();
            foreach (IModule module in station.Modules) {
                Type queryType = typeof(T);
                Type moduleType = module.GetType();

                if (queryType == moduleType) {
                    modules.Add(module as T);
                }
            }
            return modules;
        }

        public virtual T FindFirstMatch(Station station) {
            foreach (IModule module in station.Modules) {
                Type queryType = typeof(T);
                Type moduleType = module.GetType();

                if (queryType == moduleType) {
                    return module as T;
                }
            }
            return null;
        }
    }
}
