using UnityEngine;

public class HighExplosiveProjectilePool : BaseProjectilePool
{
    [SerializeField] private HighExplosiveProjectile _projectile;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private bool _autoExpand = false;

    private void Start()
    {
        Pool = new(_projectile, _poolSize, transform);
        Pool.AutoExpand = _autoExpand;
    }
}
