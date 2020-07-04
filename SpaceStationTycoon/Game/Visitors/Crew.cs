using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon.Game.Visitors
{
    public class Crew
    {
        public int Count { get; private set; }
        public Ship Ship { get; private set; }

        public Crew(int count, Ship ship) {
            Count = count;
            Ship = ship;
        }
    }
}
