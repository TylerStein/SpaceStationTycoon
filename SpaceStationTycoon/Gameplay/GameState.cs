using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Gameplay
{
    /** The GameState defines the current gameplay state
     * It is what would be serialized to save progress */
    public class GameState
    {
        public Station Station { get; set; } = null;
        public Economy Economy { get; set; } = null;
        public double Credits { get; set; } = 0.0;

        public static GameState FromScenario(Scenario scenario) {
            return new GameState() {
                Station = new Station(scenario.StartingExtraExternalUnits, scenario.StartingExtraInternalUnits, scenario.StartingModules),
                Economy = new Economy(),
                Credits = 0.0f,
            };
        }
    }
}
