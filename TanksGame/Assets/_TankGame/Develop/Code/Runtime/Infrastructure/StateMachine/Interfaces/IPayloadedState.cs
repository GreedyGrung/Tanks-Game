namespace TankGame.Runtime.Infrastructure.StateMachine.Interfaces
{
    public interface IPayloadedState<TPayload> : IBaseState
    {
        void Enter(TPayload payload);
    }
}