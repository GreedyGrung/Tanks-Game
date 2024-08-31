using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesController : MonoBehaviour
{
    public static event Action OnAllEnemiesKilled;

    [SerializeField] private List<EnemySpawner> _spawns;

    private TurretFactory _turretFactory;
    private TankFactory _tankFactory;
    private List<Enemy> _enemies = new();

    [Inject]
    private void Construct(TurretFactory turretFactory, TankFactory tankFactory)
    {
        _turretFactory = turretFactory;
        _tankFactory = tankFactory;
    }

    private void Start()
    {
        foreach (var spawn in _spawns)
        {
            Enemy enemy;
            switch (spawn.EnemyType)
            {
                case EnemyTypeId.Tank:
                    enemy = _tankFactory.GetNewInstance(spawn.transform);
                    break;
                case EnemyTypeId.Turret:
                    enemy = _turretFactory.GetNewInstance(spawn.transform);
                    break;
                case EnemyTypeId.Random:
                    int enemyType = UnityEngine.Random.Range(0, 2);
                    enemy = enemyType == 0 ? _turretFactory.GetNewInstance(spawn.transform) : _tankFactory.GetNewInstance(spawn.transform);
                    break;
                default:
                    enemy = _tankFactory.GetNewInstance(spawn.transform);
                    break;
            }

            _enemies.Add(enemy);
        }
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= RemoveEnemyFromList;
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            OnAllEnemiesKilled?.Invoke();
        }
    }
}
