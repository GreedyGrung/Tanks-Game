using TankGame.App.Projectiles;
using UnityEngine;

namespace TankGame.App.Object_Pool
{
    public class ArmorPiercingProjectilePool : BaseProjectilePool
    {
        [SerializeField] private ArmorPiercingProjectile _projectile;

        private void Start()
        {
            Pool = new(_projectile, PoolSize, transform);
            Pool.AutoExpand = AutoExpand;
        }
    }
}
