using System;
using System.Threading;

namespace SpaceStationTycoon
{
    using Scenes;
    using Gameplay;
    using Engine;

    class Program
    {
        public static readonly int gameTickMs = 100;
        
        public static ConsoleKey quitKey = ConsoleKey.Enter;

        public static Renderer renderer;
        public static Input input;
        public static Game game;

        static void Main(string[] args) {
            Console.Title = "Space Station Tycoon";

            renderer = new Renderer();
            input = new Input(quitKey, true);
            game = new Game();


            while (true) {
                InputState inputState = input.ConsumeLast();
                if (inputState.HasKey) {
                    if (inputState.Key == quitKey) {
                        break;
                    }
                }

                //rootComponent.Controller.Update(gameTickMs / 1000.0, inputState);
                //renderer.RenderView(rootComponent.View);

                game.Update(gameTickMs / 1000.0, inputState);
                renderer.RenderView(game);
                Thread.Sleep(gameTickMs);
            }

            Console.Clear();
        }     
    }
}
