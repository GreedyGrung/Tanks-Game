using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.PersistentProgress;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreateHud();
        GameObject CreateInput();
        GameObject CreatePlayer(Vector3 at);
        Task<Enemy> CreateEnemy(EnemyTypeId type, Transform parent);
        Task<SpawnPoint> CreateSpawner(EnemySpawnerData spawnerData, Player player);
        void CleanupProgressWatchers();
        Task WarmUp();
    }
}