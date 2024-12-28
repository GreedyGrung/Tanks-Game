using System;
using System.Collections.Generic;
using TankGame.App.Environment;
using TankGame.Core.Services;

namespace TankGame.App.Infrastructure.Services.SpawnersObserver
{
    public interface ISpawnersObserverService : IService
    {
        event Action OnAllEnemiesKilled;

        void Init(List<SpawnPoint> spawnPoints);
    }
}