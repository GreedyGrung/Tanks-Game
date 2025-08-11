using TankGame.Runtime.Infrastructure.StateMachine;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace TankGame.Runtime.UI.Panels
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
