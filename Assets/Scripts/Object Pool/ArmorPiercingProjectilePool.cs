using UnityEngine;

public class ArmorPiercingProjectilePool : MonoBehaviour
{
    [SerializeField] private ArmorPiercingProjectile _projectile;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private bool _autoExpand = false;

    private ObjectPool<ArmorPiercingProjectile> _pool;

    private void Start()
    {
        _pool = new(_projectile, _poolSize, transform);
        _pool.AutoExpand = _autoExpand;
    }
}
