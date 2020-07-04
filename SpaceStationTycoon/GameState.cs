using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon
{
    public enum EPurchaseResponse
    {
        OK = 0,
        INSUFFICIENT_CREDITS = 1
    }

    public class GameState
    {
        public readonly double dockIncomePerSecond = 1.0;
        public readonly double dockPrice = 10.0;

        public int docks = 1;
        public double credits = 0.0;

        public bool CanBuildDock { get => credits >= dockPrice; }
        public double TotalIncomePerSecond { get => docks * dockIncomePerSecond; }

        public GameState() {
            //
        }

        public void Update(double deltaTimeSeconds) {
            credits += (docks * dockIncomePerSecond) * deltaTimeSeconds;
        }

        public EPurchaseResponse PurchaseDock() {
            if (credits < dockPrice) {
                return EPurchaseResponse.INSUFFICIENT_CREDITS;
            }

            credits -= dockPrice;
            docks++;
            return EPurchaseResponse.OK;
        }
    }
}
