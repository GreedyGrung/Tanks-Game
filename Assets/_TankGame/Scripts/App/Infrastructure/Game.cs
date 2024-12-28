using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.UI;

namespace TankGame.App.Infrastructure
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, ServiceLocator.Instance);
        }

        public GameStateMachine StateMachine { get; private set; }
    }
}