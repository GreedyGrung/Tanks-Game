using TankGame.Runtime.Infrastructure.Services.SavingLoading;
using TankGame.Runtime.Infrastructure.StateMachine;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace TankGame.Runtime.UI.Panels
{
    public class UIVictoryPanel : UIPanelBase
    {
        private IGameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IGameStateMachine stateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }

        protected override void Close()
        {
            base.Close();

            _saveLoadService.DeleteProgress();
            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}
