using TankGame.App.Projectiles;
using UnityEngine;

namespace TankGame.App.Object_Pool
{
    public class HighExplosiveProjectilePool : BaseProjectilePool
    {
        [SerializeField] private HighExplosiveProjectile _projectile;

        private void Start()
        {
            Pool = new(_projectile, PoolSize, transform);
            Pool.AutoExpand = AutoExpand;
        }
    }
}
