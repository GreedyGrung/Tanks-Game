using System;
using System.Collections.Generic;
using TankGame.Runtime.Environment;

namespace TankGame.Runtime.Infrastructure.Services.SpawnersObserver
{
    public interface ISpawnersObserverService
    {
        event Action OnAllEnemiesKilled;

        void Init(List<SpawnPoint> spawnPoints);
    }
}