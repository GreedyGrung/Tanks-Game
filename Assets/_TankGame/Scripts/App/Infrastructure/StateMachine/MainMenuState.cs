using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.Core.Utils;

namespace TankGame.App.Infrastructure.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public MainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneNames.MainMenu);
        }

        public void Exit()
        {
           
        }
    }
}