using SpaceStationTycoon.Engine;
using System.Collections.Generic;
using System.Linq;

namespace SpaceStationTycoon.Scenes
{
    using Gameplay;
    using Gameplay.Visitors;

    class MainGameScene : Scene
    {
        private MainGameSceneController _controller;
        private MainGameView _view;

        public override IRenderable View => _view;
        public override ISceneController Controller => _controller;

        public MainGameScene(Game game, GameState initialState) {
            _controller = new MainGameSceneController(game.SceneManager, initialState);
            _view = new MainGameView(_controller);
        }
    }

    class MainGameSceneController : SceneController
    {
        private GameState _gameState;
        private SceneManager _sceneManager;

        public double Credits => _gameState.Credits;
        public List<IModule> ExternalModules => _gameState.Station.Modules.Where((IModule module) => module.IsExternal).ToList();
        public List<IModule> InternalModules => _gameState.Station.Modules.Where((IModule module) => !module.IsExternal).ToList();

        public int OccupiedInternalUnits => _gameState.Station.OccupiedInternalUnits;
        public int OccupiedExternalUnits => _gameState.Station.OccupiedExternalUnits;

        public int TotalInternalUnits => _gameState.Station.TotalInternalUnits;
        public int TotalExternalUnits => _gameState.Station.TotalExternalUnits;

        public LinkedListNode<string> FirstStationLogNode => _gameState.Station.EventLog.First;

        public MainGameSceneController(SceneManager sceneManager, GameState gameState) {
            _sceneManager = sceneManager;
            _gameState = gameState;
        }

        public override void Update(double deltaTimeSeconds, InputState inputState) {
            _gameState.Station.Update(deltaTimeSeconds);
            if (inputState.HasKey) {
                switch (inputState.Key) {
                    case System.ConsoleKey.Q:
                        _sceneManager.SetSceneWithBuilder((Game game) => new MainMenuScene(game));
                        break;
                    case System.ConsoleKey.D1:
                        AddShip1();
                        break;
                    default:
                        return;
                }
            }
        }

        public void AddShip1() {
            _gameState.Station.LogEvent("A ship approaches the station...");
            Ship newShip = new Ship(_gameState.Station, 1, 1, 2, 0, 2);
            bool didDock = newShip.DockIfDesired();
        }
    }

    class MainGameView : IRenderable
    {
        private MainGameSceneController _controller;
        public MainGameView(MainGameSceneController controller) {
            _controller = controller;
        }

        public string[] Render() {
            List<string> output = new List<string> {
                "+--------------------------------+",
                "|          Your Station          |",
                "+--------------------------------+",
                $"Credits: {_controller.Credits}",
            };

            output.Add($"External Modules ({_controller.OccupiedExternalUnits}/{_controller.TotalExternalUnits} units):");
            foreach (IModule module in _controller.ExternalModules) {
                output.Add($"> {module.DisplayStatus}");
            }


            output.Add($"Internal Modules ({_controller.OccupiedInternalUnits}/{_controller.TotalInternalUnits} units):");
            foreach (IModule module in _controller.InternalModules) {
                output.Add($"> {module.DisplayStatus}");
            }

            output.AddRange(new string[] {
                "",
                "+--------------------------------+",
                "|          Station Logs          |",
                "+--------------------------------+",
            });

            LinkedListNode<string> logNode = _controller.FirstStationLogNode;
            while (logNode != null) {
                output.Add($"> {logNode.Value}");
                logNode = logNode.Next;
            }

            return output.ToArray();
        }
    }
}
