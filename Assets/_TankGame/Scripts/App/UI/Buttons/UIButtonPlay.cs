using _TankGame.App.Infrastructure.StateMachine;
using _TankGame.App.Infrastructure.StateMachine.Interfaces;
using GreedyLogger;
using Zenject;

namespace _TankGame.App.UI.Buttons
{
    public class UIButtonPlay : UIButtonBehaviourBase
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
        }

        protected override void HandleClick()
        {
            _gameStateMachine.Enter<LoadProgressState>();
            GLogger.LogWarning("Click!", LogContext.UI);
        }
    }
}
