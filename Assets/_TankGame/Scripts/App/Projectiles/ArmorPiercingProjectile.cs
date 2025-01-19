using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.StaticData.Environment;
using UnityEngine;

namespace TankGame.App.Projectiles
{
    public class ArmorPiercingProjectile : Projectile
    {
        private ObjectPool<ArmorPiercingProjectile> _pool;

        public override void Initialize(ProjectileStaticData staticData, IPoolsService poolsService)
        {
            base.Initialize(staticData, poolsService);

            _pool = PoolsService.GetPool<ArmorPiercingProjectile>();
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();

            _pool.Return(this);
        }

        public override void Explode()
        {
            Debug.Log("ArmorPiercingProjectile explode");
        }
    }
}
