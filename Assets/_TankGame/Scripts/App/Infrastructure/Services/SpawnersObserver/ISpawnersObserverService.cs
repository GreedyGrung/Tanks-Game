using Assets.Scripts.Infrastructure;
using System;
using System.Collections.Generic;

public interface ISpawnersObserverService : IService
{
    event Action OnAllEnemiesKilled;

    void Init(List<SpawnPoint> spawnPoints);
}