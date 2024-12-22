using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemiesController : MonoBehaviour
{
    public static event Action OnAllEnemiesKilled;

    [SerializeField] private List<SpawnPoint> _spawners = new();

    private int _killedEnemies;
    private int _nonSlainSpawnersCount;
    private IUIService _uiService;

    public void Init()
    {
        FindSpawners();
    }

    public void Construct(IUIService uiService)
    {
        _uiService = uiService;
    }

    private void FindSpawners()
    {
        _spawners = FindObjectsOfType<SpawnPoint>().ToList();

        foreach (var spawnPoint in _spawners)
        {
            spawnPoint.OnEnemyInSpawnerKilled += CheckForEnemies;

            if (!spawnPoint.IsSlain)
            {
                _nonSlainSpawnersCount++;
            }
        }
    }

    private void CheckForEnemies(SpawnPoint point)
    {
        point.OnEnemyInSpawnerKilled -= CheckForEnemies;

        _killedEnemies++;

        if (_killedEnemies == _nonSlainSpawnersCount)
        {
            OnAllEnemiesKilled?.Invoke();
            _uiService.Open(UIPanelId.VictoryPanel);
        }
    }
}
