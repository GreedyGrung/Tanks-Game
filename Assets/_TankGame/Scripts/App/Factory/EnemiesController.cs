using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemiesController : MonoBehaviour
{
    public static event Action OnAllEnemiesKilled;

    [SerializeField] private List<SpawnPoint> _spawners = new();

    private int _killedEnemies;

    public void Init()
    {
        FindSpawners();
    }

    private void FindSpawners()
    {
        _spawners = FindObjectsOfType<SpawnPoint>().ToList();

        foreach (var spawnPoint in _spawners)
        {
            spawnPoint.OnEnemyInSpawnerKilled += CheckForEnemies;
        }
    }

    private void CheckForEnemies(SpawnPoint point)
    {
        point.OnEnemyInSpawnerKilled -= CheckForEnemies;

        _killedEnemies++;

        if (_killedEnemies == _spawners.Count)
        {
            OnAllEnemiesKilled?.Invoke();
        }
    }
}
