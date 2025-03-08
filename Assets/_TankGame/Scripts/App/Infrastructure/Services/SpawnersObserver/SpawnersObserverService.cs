using System;
using System.Collections.Generic;
using _TankGame.App.Environment;

namespace _TankGame.App.Infrastructure.Services.SpawnersObserver
{
    public class SpawnersObserverService : ISpawnersObserverService
    {
        public event Action OnAllEnemiesKilled;

        private int _killedEnemies;
        private int _nonSlainSpawnersCount;

        public void Init(List<SpawnPoint> spawnPoints)
        {
            _killedEnemies = 0;
            _nonSlainSpawnersCount = 0;

            FindSpawners(spawnPoints);
        }

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
            }
        }
    }
}