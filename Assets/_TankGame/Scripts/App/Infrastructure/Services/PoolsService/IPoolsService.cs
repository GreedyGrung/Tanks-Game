using _TankGame.App.Projectiles;
using _TankGame.App.Utils.Enums;

namespace _TankGame.App.Infrastructure.Services.PoolsService
{
    public interface IPoolsService
    {
        void RegisterPool<T>() where T : IPoolableObject;
        ObjectPool<T> GetPool<T>() where T : IPoolableObject;
        void ReturnToPool<T>(T poolableObject) where T : IPoolableObject;
        Projectile GetProjectile(ProjectileTypeId projectileTypeId);
        void Dispose();
    }
}
