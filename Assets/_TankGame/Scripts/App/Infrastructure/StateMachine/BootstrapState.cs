using TankGame.App.Factory;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Infrastructure.Services.SavingLoading;
using TankGame.App.Infrastructure.Services.SpawnersObserver;
using TankGame.App.Infrastructure.Services.StaticData;
using TankGame.App.Infrastructure.Services.UI;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.Core.Services.AssetManagement;
using TankGame.Core.Services.PersistentProgress;
using TankGame.Core.Utils;

namespace TankGame.App.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneNames.Bootstrap, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void RegisterServices()
        {
            RegisterStaticData();
            _serviceLocator.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterAssetProvider();
            _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(_serviceLocator.Single<IAssetProvider>(), _serviceLocator.Single<IStaticDataService>()));
            _serviceLocator.RegisterSingle<IUIFactory>(new UIFactory(_serviceLocator.Single<IAssetProvider>(), _serviceLocator.Single<IStaticDataService>()));
            _serviceLocator.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(_serviceLocator.Single<IGameFactory>(), _serviceLocator.Single<IPersistentProgressService>()));
            _serviceLocator.RegisterSingle<IUIService>(new UIService());
            _serviceLocator.RegisterSingle<ISpawnersObserverService>(new SpawnersObserverService());
            _serviceLocator.RegisterSingle<IPoolsService>(new PoolsService(_serviceLocator.Single<IGameFactory>(), _serviceLocator.Single<IStaticDataService>()));
        }

        private void RegisterAssetProvider()
        {
            var assetProvider = new AssetProvider();
            assetProvider.Initialize();
            _serviceLocator.RegisterSingle<IAssetProvider>(assetProvider);
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadEnemies();
            _serviceLocator.RegisterSingle(staticData);
        }

        private void EnterLoadLevel()
            => _stateMachine.Enter<MainMenuState>();
    }
}