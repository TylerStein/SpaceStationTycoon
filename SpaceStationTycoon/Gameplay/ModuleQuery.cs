using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Gameplay
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
                    T typedModule = module as T;
                    if (Check(typedModule)) {
                        modules.Add(typedModule);
                    }
                }
            }
            return modules;
        }

        public virtual T FindFirstMatch(Station station) {
            foreach (IModule module in station.Modules) {
                Type queryType = typeof(T);
                Type moduleType = module.GetType();

                if (queryType == moduleType) {
                    T typedModule = module as T;
                    if (Check(typedModule)) {
                        return typedModule;
                    }
                }
            }
            return null;
        }
    }
}
