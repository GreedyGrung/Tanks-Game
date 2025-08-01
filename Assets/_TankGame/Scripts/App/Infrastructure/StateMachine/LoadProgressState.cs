using TankGame.App.Infrastructure.Services.PersistentProgress;
using TankGame.App.Infrastructure.Services.PersistentProgress.Data;
using TankGame.App.Infrastructure.Services.SavingLoading;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.App.Utils;

namespace TankGame.App.Infrastructure.StateMachine
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IGameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadOrInitProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.PlayerData.PositionOnLevel.Level);
        }

        public void Exit()
        {

        }

        private void LoadOrInitProgress()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? InitNewProgress();
        }

        private PlayerProgress InitNewProgress() => new(SceneNames.Game);
    }
}