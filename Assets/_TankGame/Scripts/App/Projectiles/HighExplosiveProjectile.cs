using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.StaticData.Environment;

namespace TankGame.App.Projectiles
{
    public class HighExplosiveProjectile : Projectile
    {
        private ObjectPool<HighExplosiveProjectile> _pool;

        public override void Initialize(ProjectileStaticData staticData, IPoolsService poolsService)
        {
            base.Initialize(staticData, poolsService);

            _pool = PoolsService.GetPool<HighExplosiveProjectile>();
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();

            _pool.Return(this);
        }
    }
}
