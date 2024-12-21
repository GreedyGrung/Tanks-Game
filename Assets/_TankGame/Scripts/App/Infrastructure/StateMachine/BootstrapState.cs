using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.AssetManagement;

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
        _sceneLoader.Load(SceneNames.Bootstrap.ToString(), EnterLoadLevel);
    }

    public void Exit()
    {

    }

    private void RegisterServices()
    {
        RegisterStaticData();

        _serviceLocator.RegisterSingle<IGameStateMachine>(_stateMachine);
        _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
        _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(_serviceLocator.Single<IAssetProvider>(), _serviceLocator.Single<IStaticDataService>()));
        _serviceLocator.RegisterSingle<IUIFactory>(new UIFactory(_serviceLocator.Single<IAssetProvider>(), _serviceLocator.Single<IStaticDataService>()));
        _serviceLocator.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(_serviceLocator.Single<IGameFactory>(), _serviceLocator.Single<IPersistentProgressService>()));
        _serviceLocator.RegisterSingle<IUIService>(new UIService());
    }

    private void RegisterStaticData()
    {
        IStaticDataService staticData = new StaticDataService();
        staticData.LoadEnemies();
        _serviceLocator.RegisterSingle(staticData);
    }

    private void EnterLoadLevel() 
        => _stateMachine.Enter<LoadProgressState>();
}
