using TankGame.Runtime.Infrastructure.StateMachine.Factory;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;

namespace TankGame.Runtime.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IStateFactory _stateFactory;
        
        private IBaseState _activeState;

        public GameStateMachine(IStateFactory stateFactory) => _stateFactory = stateFactory;

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
}