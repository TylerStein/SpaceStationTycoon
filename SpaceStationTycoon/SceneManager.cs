using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon
{
    using Engine;
    using Gameplay;

    public class SceneManager
    {
        private Game _game;
   
        public bool HasLoadedScene => _currentScene != null;

        private Scene _currentScene = null;
        public Scene CurrentScene {
            get => _currentScene;
            set {
                if (HasLoadedScene) {
                    _currentScene.Controller.OnLeave();
                }
                _currentScene = value;
                _currentScene.Controller.OnEnter();
            }
        }

        public SceneManager(Game game) {
            _game = game; ;
        }

        public void SetSceneWithBuilder(Func<Gameplay.Game, Scene> builder) {
            CurrentScene = builder.Invoke(_game);
        }
    }
}
