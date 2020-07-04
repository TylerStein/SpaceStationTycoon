using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceStationTycoon.Game.Query;
using SpaceStationTycoon.Game.Modules;
namespace SpaceStationTycoon.Game
{
    class Game
    {
        public Station Station { get; set; }
        public Economy Economy { get; set; }

        public double Credits { get; set; }

        public readonly double shipSpawnCooldownSeconds = 40;
        public double ShipSpawnTimer { get; set; }

        public Game(Station station, Economy economy) {
            Station = station;
            Economy = economy;
        }

        public void Update(double deltaTimeSeconds) {
            if (ShipSpawnTimer < shipSpawnCooldownSeconds) {
                ShipSpawnTimer += deltaTimeSeconds;
                if (ShipSpawnTimer >= shipSpawnCooldownSeconds) {
                    SpawnShip();
                    ShipSpawnTimer = 0;
                }
            }
        }

        public void SpawnShip() {
            Ship newShip = new Ship(1, 1, 50, 0, 2);
            StationDockingQuery query = new StationDockingQuery(newShip, new IModuleQuery[] { new DockingQuery(1) });
            DockModule dockModule;
            if (query.AllMatch(Station, out dockModule) == true) {
                dockModule.OccupyingShip = newShip;
            }
        }
    }
}
