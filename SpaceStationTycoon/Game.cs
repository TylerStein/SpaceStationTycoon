using System;

namespace SpaceStationTycoon.Gameplay
{
    using Engine;
    using Scenes;

    public class Game : IUpdateReceiver, IRenderable
    {
        public static readonly string[] EmptyGameString = new string[] { "No scene has been loaded..." };

        public SceneManager SceneManager { get; private set; } = null;
        public GameState GameState { get; private set; } = null;

        public Game() {
            SceneManager = new SceneManager(this);
            SceneManager.CurrentScene = new MainMenuScene(this);
        }

        public void Update(double deltaTimeSeconds, InputState inputState) {
            if (SceneManager.HasLoadedScene) {
                SceneManager.CurrentScene.Controller.Update(deltaTimeSeconds, inputState);
            }
        }

        public string[] Render() {
            if (SceneManager.HasLoadedScene) {
                return SceneManager.CurrentScene.View.Render();
            }

            return EmptyGameString;
        }
    }
}
