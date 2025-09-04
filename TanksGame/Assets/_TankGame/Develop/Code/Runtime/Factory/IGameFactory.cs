using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TankGame.Runtime.Entities.Enemies.Base;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Environment;
using TankGame.Runtime.Infrastructure.Services.PersistentProgress;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.StaticData.Enemies;
using TankGame.Runtime.StaticData.Environment;
using TankGame.Runtime.Utils.Enums;
using UnityEngine;

namespace TankGame.Runtime.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        UniTask<GameObject> CreatePlayerAsync(Vector3 at);
        UniTask<GameObject> CreateHudAsync();
        UniTask<Enemy> CreateEnemyAsync(EnemyTypeId type, Transform parent);
        UniTask<SpawnPoint> CreateSpawnerAsync(EnemySpawnerData spawnerData, IPlayer player, Transform parent);
        GameObject CreateEmptyObjectWithName(string name);
        void CleanupProgressWatchers();
        ObjectPool<T> CreatePool<T>(Transform parent, ObjectPoolStaticData staticData) where T : IPoolableObject;
        T CreatePoolableObject<T>(Transform parent, bool activeByDefault) where T : IPoolableObject;
        UniTask LoadProjectiles();
        void Dispose();
        UniTask<GameObject> CreateCameraAsync();
    }
}