using TankGame.Runtime.Infrastructure.Services.ScenesLoading;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using TankGame.Runtime.Utils;

namespace TankGame.Runtime.Infrastructure.StateMachine
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