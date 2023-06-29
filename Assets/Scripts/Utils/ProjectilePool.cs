using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private int _poolSize;

    private Queue<Projectile> _projectiles = new();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CreateProjectile();
        }
    }

    private void CreateProjectile()
    {
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, transform);
        projectile.gameObject.SetActive(false);
        _projectiles.Enqueue(projectile);
    }

    private Projectile TakeFromPool()
    {
        if (_projectiles.Count == 0)
        {
            CreateProjectile();
        }

        return _projectiles.Dequeue();
    }

    private void ReturnToPool(Projectile projectile)
    {
        _projectiles.Enqueue(projectile);
    }
}
