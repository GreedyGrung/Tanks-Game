namespace TankGame.App.Infrastructure.StateMachine.Interfaces
{
    public interface IState : IBaseState
    {
        void Enter();
    }
}