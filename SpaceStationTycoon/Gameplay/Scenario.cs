using System.Collections.Generic;

namespace SpaceStationTycoon.Gameplay
{
    using Modules;

    public class Scenario
    {
        public IModule[] StartingModules { get; set; }
        public double StartingCredits { get; set; }
        public int StartingExtraInternalUnits { get; set; }
        public int StartingExtraExternalUnits { get; set; }
    }

    public static class Scenarios
    {
        public static Scenario NewGameScenario => new Scenario() {
            StartingCredits = 0.0,
            StartingExtraExternalUnits = 0,
            StartingExtraInternalUnits = 0,
            StartingModules = new IModule[] {
                new DockModule(1, 1),
                new RepairModule(1),
                new HabitationModule(1),
            },
        };
    }
}
