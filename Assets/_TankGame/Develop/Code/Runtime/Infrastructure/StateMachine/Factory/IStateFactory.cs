using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;

namespace TankGame.Runtime.Infrastructure.StateMachine.Factory
{
    public interface IStateFactory
    {
        T GetState<T>() where T : class, IBaseState;
    }
}