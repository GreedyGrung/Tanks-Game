using System;
using System.Collections.Generic;
using TankGame.App.Factory;
using TankGame.App.Infrastructure.Services.SavingLoading;
using TankGame.App.Infrastructure.Services.SpawnersObserver;
using TankGame.App.Infrastructure.Services.StaticData;
using TankGame.App.Infrastructure.Services.UI;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.App.UI;
using TankGame.Core.Services.PersistentProgress;

namespace TankGame.App.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IBaseState> _states;
        private IBaseState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, ServiceLocator serviceLocator)
        {
            _states = new Dictionary<Type, IBaseState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
                [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen,
                    serviceLocator.Single<IGameFactory>(),
                    serviceLocator.Single<IPersistentProgressService>(),
                    serviceLocator.Single<IStaticDataService>(),
                    serviceLocator.Single<IUIService>(),
                    serviceLocator.Single<IUIFactory>(),
                    serviceLocator.Single<ISpawnersObserverService>()),
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
}