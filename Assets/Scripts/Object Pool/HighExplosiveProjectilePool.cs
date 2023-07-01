using UnityEngine;

public class HighExplosiveProjectilePool : MonoBehaviour
{
    [SerializeField] private HighExplosiveProjectile _projectile;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private bool _autoExpand = false;

    private ObjectPool<HighExplosiveProjectile> _pool;

    private void Start()
    {
        _pool = new(_projectile, _poolSize, transform);
        _pool.AutoExpand = _autoExpand;
    }
}
