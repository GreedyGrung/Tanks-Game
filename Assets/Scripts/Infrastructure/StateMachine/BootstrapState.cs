using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.AssetManagement;

public class BootstrapState : IState
{
    private const string BootstrapSceneName = "BootstrapScene";
    private const string GameSceneName = "GameScene";

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
        _sceneLoader.Load(BootstrapSceneName, EnterLoadLevel);
    }

    public void Exit()
    {

    }

    private void RegisterServices()
    {
        _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
        GameFactory gameFactory = new(_serviceLocator.Single<IAssetProvider>());
        _serviceLocator.RegisterSingle<IGameFactory>(gameFactory);
    }

    private void EnterLoadLevel() 
        => _stateMachine.Enter<LoadLevelState, string>(GameSceneName);
}
