using System;
using System.Collections.Generic;
using TankGame.App.Environment;

namespace TankGame.App.Infrastructure.Services.SpawnersObserver
{
    public interface ISpawnersObserverService
    {
        event Action OnAllEnemiesKilled;

        void Init(List<SpawnPoint> spawnPoints);
    }
}