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

        Task<GameObject> CreatePlayerAsync(Vector3 at);
        Task<GameObject> CreateInputAsync();
        Task<GameObject> CreateHudAsync();
        Task<Enemy> CreateEnemyAsync(EnemyTypeId type, Transform parent);
        Task<SpawnPoint> CreateSpawnerAsync(EnemySpawnerData spawnerData, Player player);
        void CleanupProgressWatchers();
        Task WarmUp();
    }
}