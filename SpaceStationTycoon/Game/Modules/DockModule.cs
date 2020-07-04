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

        public int Tier { get; } = 1;
        public double BasePrice { get => Tier * 100; }
        public int Identifier { get => GetIdentifier(SizeClass, Tier); }
        public bool IsExternal { get => true; }
        public int Units { get => Tier * Tier; }

        public bool IsOccupied { get => OccupyingShip != null; }
        public Ship OccupyingShip { get; set; } = null;

        public int SizeClass { get; } = 1;


        public DockModule(int tier, int sizeClass) {
            if (tier < 1) throw new Exception("DockModule Tier must be >= 1");
            if (sizeClass < 1) throw new Exception("DockModule SizeClass must be >= 1");
            Tier = tier;
            SizeClass = sizeClass;
        }

        public void Update(double deltaTimeSeconds) {
            //
        }
    }

    public class DockModuleQuery : ModuleQuery<DockModule> {
        public bool IsOccupied { get; set; }
        public int MinimumTier { get; set; }
        public int SizeClass { get; set; }

        public override bool Check(DockModule module) {
            return module.IsOccupied == IsOccupied
                && module.SizeClass == SizeClass
                && module.Tier >= MinimumTier;
        }
    }

}
