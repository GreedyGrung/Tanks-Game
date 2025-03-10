using _TankGame.App.Factory;
using _TankGame.App.Infrastructure.Services.AssetManagement;
using _TankGame.App.Infrastructure.Services.Input;
using _TankGame.App.Infrastructure.Services.PersistentProgress;
using _TankGame.App.Infrastructure.Services.PoolsService;
using _TankGame.App.Infrastructure.Services.SavingLoading;
using _TankGame.App.Infrastructure.Services.SpawnersObserver;
using _TankGame.App.Infrastructure.Services.StaticData;
using _TankGame.App.Infrastructure.Services.UI;
using _TankGame.App.Infrastructure.StateMachine;
using _TankGame.App.Infrastructure.StateMachine.Interfaces;
using _TankGame.App.UI;
using UnityEngine;
using Zenject;

namespace _TankGame.App.Infrastructure
{
    public class GameBootstrapper : MonoInstaller, ICoroutineRunner, IInitializable
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
            Container.Bind<SceneLoader>().AsSingle();
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
            Container.Bind<GameLoopState>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}