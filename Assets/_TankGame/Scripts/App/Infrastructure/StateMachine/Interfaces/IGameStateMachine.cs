using TankGame.Core.Services;

namespace TankGame.App.Infrastructure.StateMachine.Interfaces
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void Enter<TState>() where TState : class, IState;
    }
}