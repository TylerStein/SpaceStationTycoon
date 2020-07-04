using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceStationTycoon.Game.Visitors;
namespace SpaceStationTycoon.Game.Modules
{
    public class RepairModule : IModule
    {
        public static int GetIdentifier(int tier) => "RepairModule".GetHashCode() ^ tier;
        public Type ModuleType { get => typeof(DockModule); }

        public int Tier { get; }
        public double BasePrice { get => Tier * 500; }
        public int Identifier { get => GetIdentifier(Tier); }
        public bool IsExternal { get => true; }
        public int Units { get => Tier; }

        public bool IsOccupied { get => OccupyingShip != null; }
        public Ship OccupyingShip { get; set; }
        
        public RepairModule(int tier) {
            if (tier < 1) throw new Exception("RepairModule Tier must be >= 1");
            Tier = tier;
        }
    }

    public struct RepairModuleQuery : IModuleQuery<RepairModule> {
        public int MinimumTier { get; set; }
        public bool IsOccupied { get; set; }

        public bool Check(RepairModule module) {
            return module.IsOccupied == IsOccupied
                   && module.Tier >= MinimumTier;
        }

        public List<RepairModule> FindAllMatches(Station station) {
            List<RepairModule> modules = new List<RepairModule>();
            foreach (IModule module in station.Modules) {
                if (module.ModuleType == typeof(RepairModule)) {
                    modules.Add(module as RepairModule);
                }
            }
            return modules;
        }

        public RepairModule FindFirstMatch(Station station) {
            foreach (IModule module in station.Modules) {
                if (module.ModuleType == typeof(RepairModule)) {
                    return module as RepairModule;
                }
            }
            return null;
        }
    }
}
