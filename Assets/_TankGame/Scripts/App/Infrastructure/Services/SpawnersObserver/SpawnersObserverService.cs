using System;
using System.Collections.Generic;

public class SpawnersObserverService : ISpawnersObserverService
{
    public event Action OnAllEnemiesKilled;

    private readonly IUIService _uiService;

    private int _killedEnemies;
    private int _nonSlainSpawnersCount;

    public SpawnersObserverService(IUIService uiService)
    {
        _uiService = uiService;
    }

    public void Init(List<SpawnPoint> spawnPoints) 
        => FindSpawners(spawnPoints);

    private void FindSpawners(List<SpawnPoint> spawnPoints)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.OnEnemyInSpawnerKilled += CheckForLeftEnemies;

            if (!spawnPoint.IsSlain)
            {
                _nonSlainSpawnersCount++;
            }
        }
    }

    private void CheckForLeftEnemies(SpawnPoint point)
    {
        point.OnEnemyInSpawnerKilled -= CheckForLeftEnemies;

        _killedEnemies++;

        if (_killedEnemies == _nonSlainSpawnersCount)
        {
            OnAllEnemiesKilled?.Invoke();
            _uiService.Open(UIPanelId.VictoryPanel);
        }
    }
}
