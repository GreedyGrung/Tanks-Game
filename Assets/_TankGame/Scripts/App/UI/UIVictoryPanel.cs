using TankGame.App.Infrastructure.Services.SavingLoading;
using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace TankGame.App.UI
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
