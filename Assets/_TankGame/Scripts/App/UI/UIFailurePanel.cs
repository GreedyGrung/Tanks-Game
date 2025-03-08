using _TankGame.App.Infrastructure.StateMachine;
using _TankGame.App.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace _TankGame.App.UI
{
    public class UIFailurePanel : UIPanelBase
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
        }

        protected override void Close()
        {
            base.Close();

            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}
