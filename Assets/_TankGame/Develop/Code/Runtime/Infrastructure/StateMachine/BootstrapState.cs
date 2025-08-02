using GreedyLogger;
using TankGame.Runtime.Infrastructure.Services.AssetManagement;
using TankGame.Runtime.Infrastructure.Services.ScenesLoading;
using TankGame.Runtime.Infrastructure.Services.StaticData;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using TankGame.Runtime.Utils;

namespace TankGame.Runtime.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;

            assetProvider.Initialize();

            GLogger.Log("Init!", LogContext.Gameplay);
        }

        public void Enter() => _sceneLoader.LoadAsync(SceneNames.Bootstrap, onLoaded: EnterLoadLevel);

        public void Exit()
        {

        }

        private async void EnterLoadLevel()
        {
            await _staticData.LoadStaticData();

            _stateMachine.Enter<MainMenuState>();
        }
    }
}