﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpaceStationTycoon
{
    /**
     * Global Economy
     *  Ships
     *      SizeClass   (Bigger docks for bigger ships)
     *      Wealth      (Can they afford to dock with your prices?)
     *      Fuel        (Do they need your refueling services?)
     *      Repairs     (Do they need your repair services?) (Tiered: Affects cost and duration)
     *      Rest        (Do they need boarding?)
     *      Risk        (What kind of folks are we dealing with?)
     *      
     *  Resources
     *      Fuel        (Required for Fuel services)
     *      Parts       (Required for Repair services)
     *      Labor       (Required for all services)
     *      Commodities (tiered)
     *          Luxuries
     *          Tools
     *          Contraband
     *  
     * Visitor
     *   Interest scale for commodoties, will only buy if good price/interest value
     *      
     * Station Modules
     *  - Station has a number of Units, Modules take up a set number of Units
     *  - Can buy more Units to allow more Modules to be built
     *  - Station requires Fuel scaled by the number of Units
     *  
     *  Dock
     *      - Small (2 Units, small ships)
     *      - Medium (4 Units, med ships)
     *      - Large (8 units, beeg sheeps)
     *  Habitation
     *      - Temporary Habitat (2 units, 10 Visitors)
     *  
     *  Market
     *      - Small (T1 comd. only, limit transactions (x customers /second))
     *      
     *  Storage
     *      - Commodity Storage
     *      - Fuel Storage
     *      - Labor Storage
     *      - Parts Storage
     *  
     */

    class Program
    {
        public static readonly double gameTickSeconds = 0.25;

        public static View view;
        public static Tick loop;
        public static GameState game;        

        static void Main(string[] args) {
            loop = new Tick(gameTickSeconds, OnTick, OnDispose);
            view = new View();
            game = new GameState();

            while (true) {
                var key = Console.ReadKey(true);
                if (game.CanBuildDock && key.Key == ConsoleKey.D1) {
                    game.PurchaseDock();
                }
                if (key.Key == ConsoleKey.Enter) {
                    break;
                }
            }
        }

        static void OnTick(double time) {
            game.Update(gameTickSeconds);
            view.RenderGameState(game);
        }

        static void OnDispose() {

        }
    }
}
