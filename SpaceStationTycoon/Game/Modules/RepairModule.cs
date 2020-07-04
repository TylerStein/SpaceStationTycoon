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

        public int Tier { get; } = 1;
        public double BasePrice { get => Tier * 500; }
        public int Identifier { get => GetIdentifier(Tier); }
        public bool IsExternal { get => true; }
        public int Units { get => Tier; }

        public bool IsOccupied { get => OccupyingShip != null; }
        public Ship OccupyingShip { get; private set; }

        public double RemainingRepairTime { get => RepairTimeTarget - RepairTimer; }
        public double RepairTimer { get; private set; } = 0.0;
        public double RepairTimeTarget { get; private set; } = 0.0;
        
        public RepairModule(int tier) {
            if (tier < 1) throw new Exception("RepairModule Tier must be >= 1");
            Tier = tier;
        }

        public void OccupyWithShip(Ship ship) {
            OccupyingShip = ship;
            RepairTimeTarget = ship.Tier * ship.Tier * 10;
            RepairTimer = 0;
        }

        public void Update(double deltaTimeSeconds) {
            if (IsOccupied) {
                RepairTimer += deltaTimeSeconds;
                if (RepairTimer >= RepairTimeTarget) {
                    OccupyingShip.RemoveShipModuleUse(this);
                    OccupyingShip = null;
                }
            }
        }
    }

    public class RepairModuleQuery : ModuleQuery<RepairModule> {
        public int MinimumTier { get; set; }
        public bool IsOccupied { get; set; }

        public override bool Check(RepairModule module) {
            return module.IsOccupied == IsOccupied
                   && module.Tier >= MinimumTier;
        }
    }
}
