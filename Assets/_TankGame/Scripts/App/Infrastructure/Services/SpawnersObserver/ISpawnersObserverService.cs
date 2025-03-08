using System;
using System.Collections.Generic;
using _TankGame.App.Environment;

namespace _TankGame.App.Infrastructure.Services.SpawnersObserver
{
    public interface ISpawnersObserverService
    {
        event Action OnAllEnemiesKilled;

        void Init(List<SpawnPoint> spawnPoints);
    }
}