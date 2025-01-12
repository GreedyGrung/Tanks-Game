using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.App.Infrastructure;
using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.Infrastructure.Services.SavingLoading;

namespace TankGame.App.UI
{
    public class UIVictoryPanel : UIPanelBase
    {
        private IGameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;

        protected override void Initialize()
        {
            base.Initialize();

            _gameStateMachine = ServiceLocator.Instance.Single<IGameStateMachine>();
            _saveLoadService = ServiceLocator.Instance.Single<ISaveLoadService>();
        }

        protected override void Close()
        {
            base.Close();

            _saveLoadService.DeleteProgress();
            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}
