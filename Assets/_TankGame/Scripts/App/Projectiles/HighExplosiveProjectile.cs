namespace TankGame.App.Projectiles
{
    public class HighExplosiveProjectile : Projectile
    {
        protected override void ReturnToPool()
        {
            base.ReturnToPool();

            PoolsService.ReturnToPool(this);
        }
    }
}
