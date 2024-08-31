using UnityEngine;

public class ArmorPiercingProjectilePool : BaseProjectilePool
{
    [SerializeField] private ArmorPiercingProjectile _projectile;

    private void Start()
    {
        Pool = new(_projectile, PoolSize, transform);
        Pool.AutoExpand = AutoExpand;
    }
}
