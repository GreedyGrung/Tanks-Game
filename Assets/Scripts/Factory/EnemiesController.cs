using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [Header("Factories")]
    [SerializeField] private TurretFactory _turretFactory;
    [SerializeField] private TankFactory _tankFactory;

    [SerializeField] private List<Transform> _spawns;

    private List<Enemy> _enemies = new();

    private void Start()
    {
        foreach (var spawn in _spawns)
        {
            int enemyType = Random.Range(0, 2);
            Enemy enemy = enemyType == 0 ? _turretFactory.GetNewInstance(spawn) : _tankFactory.GetNewInstance(spawn);
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
            Debug.Log("Game ended");
        }
    }
}
