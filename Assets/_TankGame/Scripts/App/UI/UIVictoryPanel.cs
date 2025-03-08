using _TankGame.App.Infrastructure.Services.SavingLoading;
using _TankGame.App.Infrastructure.StateMachine;
using _TankGame.App.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace _TankGame.App.UI
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
