using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceStationTycoon.Gameplay.Modules;
namespace SpaceStationTycoon.Gameplay.Visitors
{
    public class Ship
    {
        public Station Station { get; private set; } = null;
        public int Tier { get; private set; } = 1;
        public int SizeClass { get; private set; } = 1;
        public Crew Crew { get; private set; } = null;
        public int RequiredFuelLevel { get; private set; } = 0;
        public int RequiredRepairLevel { get; private set; } = 0;

        public List<IModule> activeShipModules { get; private set; } = new List<IModule>();

        public Ship(Station station, int tier, int sizeClass, int crewCount, int requiredFuelLevel, int requiredRepairLevel) {
            Station = station;
            Tier = tier;
            SizeClass = sizeClass;
            Crew = new Crew(crewCount, this);
            RequiredFuelLevel = requiredFuelLevel;
            RequiredRepairLevel = requiredRepairLevel;
        }

        public void RemoveShipModuleUse(IModule sourceModule) {
            activeShipModules.Remove(sourceModule);
            LeaveIfReady();
        }

        public void LeaveIfReady() {
           // if (activeCrewModules.Count > 0) return;
            if (activeShipModules.Count > 1) return;
            (activeShipModules[0] as DockModule).OccupyingShip = null;
            Station.LogEvent("A docked ship leaves");
        }

        public bool DockIfDesired() {
            // Is there available Docking space accomodating this ship?
            DockModuleQuery dockModuleQuery = new DockModuleQuery { IsOccupied = false, MinimumTier = Tier, SizeClass = SizeClass };
            List<DockModule> dockingSpaces = dockModuleQuery.FindAllMatches(Station);
            List<Action> onSuccess = new List<Action>();
            List<IModule> modulesUsed = new List<IModule>();

            if (dockingSpaces.Count == 0) {
                Station.LogEvent("The ship passes by after finding your station does not meet it's docking needs.");
                return false;
            } else {
                modulesUsed.Add(dockingSpaces[0]);
                onSuccess.Add(() => dockingSpaces[0].OccupyingShip = this);
            }

            // TODO: Fuel?

            // Is there available repair services for this ship?
            if (RequiredRepairLevel > 0) {
                RepairModuleQuery repairModuleQuery = new RepairModuleQuery { MinimumTier = Tier };
                RepairModule repairModule = repairModuleQuery.FindFirstMatch(Station);
                if (repairModule == null) {
                    Station.LogEvent("The ship passes by after finding your station does not meet it's repair needs.");
                    return false;
                } else {
                    modulesUsed.Add(repairModule);
                    onSuccess.Add(() => repairModule.OccupyWithShip(this));
                }

                if (RequiredRepairLevel > 1) {
                    // Repair level requires boarding
                    HabitationModuleQuery habitationModuleQuery = new HabitationModuleQuery { MinimumSpace = Crew.Count, MinimumTier = Tier };
                    HabitationModule habitationModule = habitationModuleQuery.FindFirstMatch(Station);
                    if (habitationModule == null) {
                        Station.LogEvent("The ship passes by after finding your station does not meet it's crew habitation needs.");
                        return false;
                    } else {
                        modulesUsed.Add(habitationModule);
                        onSuccess.Add(() => habitationModule.AddCrew(Crew));
                    }
                }
            }

            // update all the modules with the docking ship (this could be done waaaay better)
            foreach (Action action in onSuccess) {
                action.Invoke();
            }

            List<string> moduleNames = modulesUsed.Select((IModule module) => {
                activeShipModules.Add(module);
                return module.GetType().Name;
            }).ToList();

            Station.LogEvent($"The ship arrived and is making use of the following services: [{string.Join(", ", moduleNames)}]");
            return true;
        }
    }
}
