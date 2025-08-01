using TankGame.App.Infrastructure.Services.ScenesLoading;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.App.Utils;

namespace TankGame.App.Infrastructure.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public MainMenuState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadAsync(SceneNames.MainMenu);
        }

        public void Exit()
        {
           
        }
    }
}