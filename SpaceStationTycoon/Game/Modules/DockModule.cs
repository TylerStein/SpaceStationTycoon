using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceStationTycoon.Game.Visitors;
namespace SpaceStationTycoon.Game.Modules
{
    public class DockModule : IModule {
        public static int GetIdentifier(int sizeClass, int tier) => "DockModule".GetHashCode() ^ sizeClass ^ tier;
        public Type ModuleType { get => typeof(DockModule); }

        public int Tier { get; }
        public double BasePrice { get => Tier * 100; }
        public int Identifier { get => GetIdentifier(SizeClass, Tier); }
        public bool IsExternal { get => true; }
        public int Units { get => Tier * Tier; }

        public bool IsOccupied { get => OccupyingShip != null; }
        public Ship OccupyingShip { get; set; }

        public int SizeClass { get; }


        public DockModule(int tier, int sizeClass) {
            if (tier < 1) throw new Exception("DockModule Tier must be >= 1");
            if (sizeClass < 1) throw new Exception("DockModule SizeClass must be >= 1");
            Tier = tier;
            SizeClass = sizeClass;
        }
    }

    public struct DockModuleQuery : IModuleQuery<DockModule> {
        public bool IsOccupied { get; set; }
        public int MinimumTier { get; set; }
        public int SizeClass { get; set; }

        public bool Check(DockModule module) {
            return module.IsOccupied == IsOccupied
                && module.SizeClass == SizeClass
                && module.Tier >= MinimumTier;
        }

        public List<DockModule> FindAllMatches(Station station) {
            List<DockModule> modules = new List<DockModule>();
            foreach (IModule module in station.Modules) {
                if (module.ModuleType == typeof(DockModule)) {
                    modules.Add(module as DockModule);
                }
            }
            return modules;
        }

        public DockModule FindFirstMatch(Station station) {
            foreach (IModule module in station.Modules) {
                if (module.ModuleType == typeof(DockModule)) {
                    return module as DockModule;
                }
            }
            return null;
        }
    }

}
