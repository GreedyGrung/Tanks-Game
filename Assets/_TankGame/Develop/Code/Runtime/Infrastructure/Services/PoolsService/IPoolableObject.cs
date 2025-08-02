namespace TankGame.Runtime.Infrastructure.Services.PoolsService
{
    public interface IPoolableObject
    {
        void OnSpawned();
        void OnDespawned();
    }
}
