using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace TankGame.Runtime.Infrastructure.StateMachine.Factory
{
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
}