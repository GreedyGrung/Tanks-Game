namespace Assets.Scripts.Infrastructure
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;

        public static ServiceLocator Instance 
            => _instance ??= new ServiceLocator();

        public void RegisterSingle<TService>(TService service) where TService : IService
        {
            Implemenatation<TService>.Instance = service;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implemenatation<TService>.Instance;
        }

        private static class Implemenatation<TService> where TService : IService
        {
            public static TService Instance;
        }
    }
}
