using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceStationTycoon.Game.Modules;
namespace SpaceStationTycoon.Game.Visitors
{
    public class Ship
    {
        public int Tier { get; private set; }
        public int SizeClass { get; private set; }
        public int CrewCount { get; private set; }
        public int RequiredFuelLevel { get; private set; }
        public int RequiredRepairLevel { get; private set; }

        public Ship(int tier, int sizeClass, int requiredFuelLevel, int requiredRepairLevel, int crewCount) {
            Tier = tier;
            SizeClass = sizeClass;
            RequiredFuelLevel = requiredFuelLevel;
            RequiredRepairLevel = requiredRepairLevel;
            CrewCount = crewCount;
        }

        public static bool WillDock(Station station, Ship ship) {
            // Is there available Docking space accomodating this ship?
            DockModuleQuery dockModuleQuery = new DockModuleQuery { IsOccupied = false, MinimumTier = ship.Tier, SizeClass = ship.SizeClass };
            List<DockModule> dockingSpaces = dockModuleQuery.FindAllMatches(station);
            if (dockingSpaces.Count == 0) return false;

            // Is there available repair services for this ship?
            if (ship.RequiredRepairLevel > 0) {
                RepairModuleQuery repairModuleQuery = new RepairModuleQuery { MinimumTier = ship.Tier }

            }



            return false;
        }
    }
}
