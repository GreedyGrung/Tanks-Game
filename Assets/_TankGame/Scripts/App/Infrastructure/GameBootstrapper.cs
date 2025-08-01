using TankGame.App.Factory;
using TankGame.App.Infrastructure.Services.AssetManagement;
using TankGame.App.Infrastructure.Services.Input;
using TankGame.App.Infrastructure.Services.PersistentProgress;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Infrastructure.Services.SavingLoading;
using TankGame.App.Infrastructure.Services.SpawnersObserver;
using TankGame.App.Infrastructure.Services.StaticData;
using TankGame.App.Infrastructure.Services.UI;
using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.App.UI;
using UnityEngine;
using Zenject;

namespace TankGame.App.Infrastructure
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