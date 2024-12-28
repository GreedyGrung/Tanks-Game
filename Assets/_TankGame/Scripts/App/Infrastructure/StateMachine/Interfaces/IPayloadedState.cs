namespace TankGame.App.Infrastructure.StateMachine.Interfaces
{
    public interface IPayloadedState<TPayload> : IBaseState
    {
        void Enter(TPayload payload);
    }
}