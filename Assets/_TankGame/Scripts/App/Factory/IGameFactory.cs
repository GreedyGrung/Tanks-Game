using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.App.Entities.Enemies.Base;
using TankGame.App.Entities.Interfaces;
using TankGame.App.Environment;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.ObjectPool;
using TankGame.App.StaticData;
using TankGame.Core.Services;
using TankGame.Core.Services.PersistentProgress;
using TankGame.Core.Utils.Enums;
using UnityEngine;

namespace TankGame.App.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        Task<GameObject> CreatePlayerAsync(Vector3 at);
        Task<GameObject> CreateInputAsync();
        Task<GameObject> CreateHudAsync();
        Task<Enemy> CreateEnemyAsync(EnemyTypeId type, Transform parent);
        Task<SpawnPoint> CreateSpawnerAsync(EnemySpawnerData spawnerData, IPlayer player);
        GameObject CreateEmptyObjectWithName(string name);
        void CleanupProgressWatchers();
        Task WarmUp();
        ObjectPool<T> CreatePool<T>(Transform parent, ObjectPoolStaticData staticData) where T : IPoolableObject;
        Task<T> CreatePoolableObject<T>(Transform parent, bool activeByDefault) where T : IPoolableObject;
    }
}