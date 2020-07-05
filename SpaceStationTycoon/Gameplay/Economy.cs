using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Gameplay
{
    /**
     * The Economy determines:
     *  - The Visitors that may arrive at the Station
     *  - The availability and prices of Resources
     *  - Special world events (TBD)
     */
    public class Economy
    {
        private double _priceOfFuelPerUnit = 1.0;

        private double _priceOfPartsPerUnit = 1.0;

        private double _priceOfLaborerPerUnit = 1.0;

        private double _priceOfLuxuryCommoditiesPerUnit = 1.0;
        private double _priceOfToolCommoditiesPerUnit = 1.0;
        private double _priceOfContrabandCommoditiesPerUnit = 1.0;

        private double _averageSmallShipTraffic = 1.0;
        private double _averageMediumShipTraffic = 0.5;
        private double _averageLargeShipTraffic = 0.25;

        private double _averageVisitorWealth = 0.5;
        private double _averageVisitorRisk = 0.5;

        private double _averageRepairNeeds = 0.5;
        private double _averageFuelNeeds = 0.5;
        private double _averageRestNeeds = 0.5;
    }
}
