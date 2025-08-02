using TankGame.Runtime.Infrastructure.Services.ScenesLoading;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using TankGame.Runtime.UI;
using TankGame.Runtime.Utils;

namespace TankGame.Runtime.Infrastructure.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;

        public MainMenuState(ISceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
        }

        public void Enter()
        {
            _sceneLoader.LoadAsync(SceneNames.MainMenu, () => _loadingScreen.Hide());
        }

        public void Exit()
        {
           
        }
    }
}