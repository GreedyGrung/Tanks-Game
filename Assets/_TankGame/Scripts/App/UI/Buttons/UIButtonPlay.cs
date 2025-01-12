using TankGame.App.Infrastructure;
using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.Infrastructure.StateMachine.Interfaces;

namespace TankGame.App.UI.Buttons
{
    public class UIButtonPlay : UIButtonBehaviourBase
    {
        private IGameStateMachine _gameStateMachine;

        protected override void Initialize()
        {
            base.Initialize();

            _gameStateMachine = ServiceLocator.Instance.Single<IGameStateMachine>();
        }

        protected override void HandleClick()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}
