using TankGame.App.Infrastructure;
using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.Infrastructure.StateMachine.Interfaces;

namespace TankGame.App.UI
{
    public class UIFailurePanel : UIPanelBase
    {
        private IGameStateMachine _gameStateMachine;

        protected override void Initialize()
        {
            base.Initialize();

            _gameStateMachine = ServiceLocator.Instance.Single<IGameStateMachine>();
        }

        protected override void Close()
        {
            base.Close();

            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}
