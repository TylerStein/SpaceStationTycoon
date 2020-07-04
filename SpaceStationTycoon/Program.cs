using System;
using System.Threading;

namespace SpaceStationTycoon
{
    using Game;
    using Game.Modules;

    class Program
    {
        public static readonly double gameTickSeconds = 0.25;

        public static View view;
        // public static Tick loop;
        public static GameInstance game; 

        static void Main(string[] args) {
            // loop = new Tick(gameTickSeconds, OnTick, OnDispose);
            view = new View();

            Economy economy = new Economy();
            Station station = new Station(0, 0, new IModule[] { new DockModule(1, 1), new RepairModule(1), new HabitationModule(1) });
            game = new GameInstance(station, economy);

            int gameTickMs = (int)Math.Round(gameTickSeconds * 1000);
            Thread gameThread = new Thread(() => {
                Thread.CurrentThread.IsBackground = false;
                while (true) {
                    Thread.Sleep(gameTickMs);
                    OnTick(gameTickSeconds);
                }
            });
            gameThread.Start();

            while (true) {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) {
                    break;
                }
            }

            gameThread.Abort();
            gameThread = null;
        }

        static void OnTick(double deltaTimeMs) {
            game.Update(gameTickSeconds);
            view.RenderGameState(game);
        }
    }
}
