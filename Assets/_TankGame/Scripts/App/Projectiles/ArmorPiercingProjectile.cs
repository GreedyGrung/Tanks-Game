using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.StaticData.Environment;

namespace TankGame.App.Projectiles
{
    public class ArmorPiercingProjectile : Projectile
    {
        private ObjectPool<ArmorPiercingProjectile> _pool;

        public override void Initialize(ProjectileStaticData staticData)
        {
            base.Initialize(staticData);

            _pool = PoolsService.GetPool<ArmorPiercingProjectile>();
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();

            _pool.Return(this);
        }
    }
}
