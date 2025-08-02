using TankGame.Runtime.Projectiles;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Infrastructure.Services.PoolsService
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
