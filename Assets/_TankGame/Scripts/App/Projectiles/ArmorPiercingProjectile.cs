using _TankGame.App.StaticData.Environment;

namespace _TankGame.App.Projectiles
{
    public class ArmorPiercingProjectile : Projectile
    {
        public override void Initialize(ProjectileStaticData staticData)
        {
            base.Initialize(staticData);
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();

            PoolsService.ReturnToPool(this);
        }
    }
}
