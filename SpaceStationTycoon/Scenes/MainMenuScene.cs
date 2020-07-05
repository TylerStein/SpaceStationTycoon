using SpaceStationTycoon.Engine;

namespace SpaceStationTycoon.Scenes
{
    using Gameplay;

    class MainMenuScene : Scene
    {
        private MainMenuSceneController _controller;
        private MainMenuView _view;

        public override IRenderable View => _view;
        public override ISceneController Controller => _controller;

        public MainMenuScene(Game game) {
            _controller = new MainMenuSceneController(game.SceneManager);
            _view = new MainMenuView(_controller);
        }
    }

    class MainMenuSceneController : SceneController
    {
        private SceneManager _sceneManager;
        public MainMenuSceneController(SceneManager sceneManager) {
            _sceneManager = sceneManager;
        }

        public override void Update(double deltaTimeSeconds, InputState inputState) {
            if (inputState.HasKey) {
                switch (inputState.Key) {
                    case System.ConsoleKey.D1:
                        OnNewGame();
                        break;
                    case System.ConsoleKey.D2:
                        // TODO: Loading
                        break;
                    default:
                        return;
                }
            }
        }

        public void OnNewGame() {
            GameState newGameState = GameState.FromScenario(Scenarios.NewGameScenario);
            _sceneManager.SetSceneWithBuilder((Game game) => new MainGameScene(game, newGameState));
        }
    }

    class MainMenuView : IRenderable
    {
        private MainMenuSceneController _controller;
        public MainMenuView(MainMenuSceneController controller) {
            _controller = controller;
        }

        public string[] Render() {
            return new string[] {
                "+--------------------------+",
                "|   Space Station Tycoon   |",
                "+--------------------------+",
                "|                          |",
                "|   [1] - New Game         |",
                "|   [2] - Load Game (WIP)  |",
                "|                          |",
                "+--------------------------+",
            };
        }
    }
}
