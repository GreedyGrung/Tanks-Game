using TankGame.Runtime.Infrastructure.Services.PersistentProgress;
using TankGame.Runtime.Infrastructure.Services.StaticData;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using TankGame.Runtime.Utils;

namespace TankGame.Runtime.Infrastructure.StateMachine
{
    public class LoadNewGameState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;

        public LoadNewGameState(IGameStateMachine gameStateMachine, IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            InitProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.PlayerData.PositionOnLevel.Level);
        }

        public void Exit()
        {

        }

        private void InitProgress()
        {
            _progressService.Progress = new(SceneNames.Game);
            var initialPosition = _staticDataService.ForLevel(SceneNames.Game).PlayerPosition.AsVectorData();
            _progressService.Progress.PlayerData.PositionOnLevel = new(SceneNames.Game, initialPosition);
        }
    }
}