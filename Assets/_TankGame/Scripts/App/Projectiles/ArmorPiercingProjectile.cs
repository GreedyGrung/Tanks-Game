namespace TankGame.App.Projectiles
{
    public class ArmorPiercingProjectile : Projectile
    {
        protected override void ReturnToPool()
        {
            base.ReturnToPool();

            PoolsService.ReturnToPool(this);
        }
    }
}
