using UnityEngine;

namespace TankGame.App.Projectiles
{
    public class HighExplosiveProjectile : Projectile
    {
        public override void Explode()
        {
            Debug.Log("HighExplosiveProjectile explode");
        }
    }
}
