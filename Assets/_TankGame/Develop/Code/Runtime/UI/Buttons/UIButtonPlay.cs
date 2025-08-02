using GreedyLogger;
using TankGame.Runtime.Infrastructure.StateMachine;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace TankGame.Runtime.UI.Buttons
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
