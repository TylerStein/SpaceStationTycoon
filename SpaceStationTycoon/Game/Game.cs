namespace SpaceStationTycoon.Game
{
    using Visitors;

    public class GameInstance
    {
        public Station Station { get; set; } = null;
        public Economy Economy { get; set; } = null;

        public double Credits { get; set; } = 0.0;

        public readonly double shipSpawnCooldownSeconds = 8.0;
        public double ShipSpawnTimer { get; set; } = 5.5;

        public GameInstance(Station station, Economy economy) {
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

            Station.Update(deltaTimeSeconds);
        }

        public void SpawnShip() {
            Station.LogEvent("A ship nears the station");
            Ship ship = new Ship(Station, 1, 1, 2, 0, 2);
            ship.DockIfDesired();
        }
    }
}
