using TankGame.Runtime.Factory;
using TankGame.Runtime.Infrastructure.Services.AssetManagement;
using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.Infrastructure.Services.Pause;
using TankGame.Runtime.Infrastructure.Services.PersistentProgress;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.Infrastructure.Services.SavingLoading;
using TankGame.Runtime.Infrastructure.Services.ScenesLoading;
using TankGame.Runtime.Infrastructure.Services.SpawnersObserver;
using TankGame.Runtime.Infrastructure.Services.StaticData;
using TankGame.Runtime.Infrastructure.Services.UI;
using TankGame.Runtime.Infrastructure.StateMachine;
using TankGame.Runtime.Infrastructure.StateMachine.Factory;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using TankGame.Runtime.UI;
using UnityEngine;
using Zenject;

namespace TankGame.Runtime.Infrastructure
{
    public class GameBootstrapper : MonoInstaller, IInitializable
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        public override void InstallBindings()
        {
            BindGameBootstrapper();
            BindLoadingScreen();
            BindSceneLoader();
            BindGameStateMachine();
            BindAssetsManagementServices();
            BindFactories();
            BindProgressServices();
            BindUIServices();
            BindGameplayServices();
            BindInputService();
            BindGameStates();
        }

        private void BindGameBootstrapper()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().FromInstance(this).AsSingle();
        }

        private void BindLoadingScreen()
        {
            Container.Bind<LoadingScreen>().FromInstance(_loadingScreen).AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void BindAssetsManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ISpawnersObserverService>().To<SpawnersObserverService>().AsSingle();
            Container.Bind<IPoolsService>().To<PoolsService>().AsSingle();
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
        }

        private void BindInputService()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.Bind<IUIService>().To<UIService>().AsSingle();
        }

        private void BindProgressServices()
        {
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadNewGameState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}