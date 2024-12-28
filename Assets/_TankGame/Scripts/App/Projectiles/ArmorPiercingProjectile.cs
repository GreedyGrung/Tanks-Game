using UnityEngine;

namespace TankGame.App.Projectiles
{
    public class ArmorPiercingProjectile : Projectile
    {
        public override void Explode()
        {
            Debug.Log("ArmorPiercingProjectile explode");
        }
    }
}
