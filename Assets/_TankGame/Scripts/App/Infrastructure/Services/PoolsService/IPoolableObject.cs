namespace _TankGame.App.Infrastructure.Services.PoolsService
{
    public interface IPoolableObject
    {
        void OnSpawned();
        void OnDespawned();
    }
}
