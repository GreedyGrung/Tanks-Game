using UnityEngine;

public class HighExplosiveProjectilePool : BaseProjectilePool
{
    [SerializeField] private HighExplosiveProjectile _projectile;

    private void Start()
    {
        Pool = new(_projectile, PoolSize, transform);
        Pool.AutoExpand = AutoExpand;
    }
}
