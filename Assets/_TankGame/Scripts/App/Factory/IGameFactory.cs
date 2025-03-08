using System.Collections.Generic;
using System.Threading.Tasks;
using _TankGame.App.Entities.Enemies.Base;
using _TankGame.App.Entities.Interfaces;
using _TankGame.App.Environment;
using _TankGame.App.Infrastructure.Services.PersistentProgress;
using _TankGame.App.Infrastructure.Services.PoolsService;
using _TankGame.App.StaticData.Enemies;
using _TankGame.App.StaticData.Environment;
using _TankGame.App.Utils.Enums;
using UnityEngine;

namespace _TankGame.App.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        Task<GameObject> CreatePlayerAsync(Vector3 at);
        Task<GameObject> CreateHudAsync();
        Task<Enemy> CreateEnemyAsync(EnemyTypeId type, Transform parent);
        Task<SpawnPoint> CreateSpawnerAsync(EnemySpawnerData spawnerData, IPlayer player, Transform parent);
        GameObject CreateEmptyObjectWithName(string name);
        void CleanupProgressWatchers();
        Task WarmUp();
        ObjectPool<T> CreatePool<T>(Transform parent, ObjectPoolStaticData staticData) where T : IPoolableObject;
        Task<T> CreatePoolableObject<T>(Transform parent, bool activeByDefault) where T : IPoolableObject;
    }
}