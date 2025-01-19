using TankGame.App.Projectiles;
using TankGame.Core.Services;
using TankGame.Core.Utils.Enums;

namespace TankGame.App.Infrastructure.Services.PoolsService
{
    public interface IPoolsService : IService
    {
        void RegisterPool<T>() where T : IPoolableObject;
        ObjectPool<T> GetPool<T>() where T : IPoolableObject;
        Projectile GetProjectile(ProjectileTypeId projectileTypeId);
        void Dispose();
    }
}
