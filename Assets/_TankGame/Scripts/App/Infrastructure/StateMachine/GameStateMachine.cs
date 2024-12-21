using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure;
using System;
using System.Collections.Generic;

public class GameStateMachine : IGameStateMachine
{
    private readonly Dictionary<Type, IBaseState> _states;
    private IBaseState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, ServiceLocator serviceLocator)
    {
        _states = new Dictionary<Type, IBaseState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen,
                serviceLocator.Single<IGameFactory>(),
                serviceLocator.Single<IPersistentProgressService>(),
                serviceLocator.Single<IStaticDataService>(),
                serviceLocator.Single<IUIService>(),
                serviceLocator.Single<IUIFactory>()),
            [typeof(LoadProgressState)] = new LoadProgressState(this, serviceLocator.Single<IPersistentProgressService>(), serviceLocator.Single<ISaveLoadService>()),
            [typeof(GameLoopState)] = new GameLoopState(this)
        };
    }

    public void Enter<TState>() where TState : class, IState
        => ChangeState<TState>().Enter();

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        => ChangeState<TState>().Enter(payload);

    private TState ChangeState<TState>() where TState : class, IBaseState
    {
        _activeState?.Exit();

        var state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IBaseState
        => _states[typeof(TState)] as TState;
}
