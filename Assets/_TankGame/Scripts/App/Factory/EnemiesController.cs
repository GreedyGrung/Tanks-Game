using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemiesController : MonoBehaviour
{
    public static event Action OnAllEnemiesKilled;

    [SerializeField] private Transform _spawnersRoot;

    [SerializeField] private List<Enemy> _enemies = new();
    private List<EnemySpawner> _spawners = new();

    public void Init()
    {
        FindSpawners();
        GetEnemies();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= RemoveEnemyFromList;
    }

    private void FindSpawners()
    {
        foreach (Transform transform in _spawnersRoot)
        {
            if (transform.TryGetComponent(out EnemySpawner spawner))
            {
                _spawners.Add(spawner);
            }
        }
    }

    private void GetEnemies()
    {
        Debug.LogError("get");
        _enemies.AddRange(_spawners
            .Where(spawner => spawner.Enemy != null)
            .Select(spawner => spawner.Enemy));
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy);
        _spawners.First(spawner => spawner.Enemy == enemy).SetIsSlain();

        if (_enemies.Count == 0)
        {
            OnAllEnemiesKilled?.Invoke();
        }
    }
}
