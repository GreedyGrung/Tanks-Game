using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace TankGame.Runtime.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private IBaseState _activeState;
        private IStateFactory _stateFactory;

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Enter<TState>() where TState : class, IState
            => ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
            => ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IBaseState
        {
            _activeState?.Exit();

            var state = _stateFactory.GetState<TState>();
            _activeState = state;

            return state;
        }
    }

    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public T GetState<T>() where T : class, IBaseState
        {
            return _container.Resolve<T>();
        }
    }

    public interface IStateFactory
    {
        T GetState<T>() where T : class, IBaseState;
    }
}