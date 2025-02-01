using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.App.Entities.Enemies.Base;
using TankGame.App.Entities.Interfaces;
using TankGame.App.Environment;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Infrastructure.Services.StaticData;
using TankGame.App.Projectiles;
using TankGame.App.StaticData.Enemies;
using TankGame.App.StaticData.Environment;
using TankGame.Core.Services.AssetManagement;
using TankGame.Core.Services.PersistentProgress;
using TankGame.Core.Utils;
using TankGame.Core.Utils.Enums;
using UnityEngine;
using Zenject;

namespace TankGame.App.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, DiContainer container)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _container = container;
        }

        public async Task WarmUp()
            => await _assetProvider.Load<GameObject>(Constants.SpawnerAddress);

        public async Task<GameObject> CreatePlayerAsync(Vector3 at)
            => await InstantiateRegisteredAsync(Constants.PlayerAddress, at);

        public async Task<GameObject> CreateInputAsync()
            => await InstantiateRegisteredAsync(Constants.UnityInputActionsAddress);

        public async Task<GameObject> CreateHudAsync()
            => await InstantiateRegisteredAsync(Constants.HudAddress);

        public async Task<Enemy> CreateEnemyAsync(EnemyTypeId type, Transform parent)
        {
            var enemyData = _staticData.ForEnemy(type);
            GameObject prefab = await _assetProvider.Load<GameObject>(enemyData.PrefabReference);

            var enemy = UnityEngine.Object.Instantiate(prefab, parent.position, Quaternion.identity, parent).GetComponent<Enemy>();
            enemy.SetData(enemyData);

            return enemy;
        }

        public async Task<SpawnPoint> CreateSpawnerAsync(EnemySpawnerData spawnerData, IPlayer player, Transform parent)
        {
            var prefab = await _assetProvider.Load<GameObject>(Constants.SpawnerAddress);
            var spawner = _container.InstantiatePrefab(prefab, spawnerData.Position, Quaternion.identity, parent).GetComponent<SpawnPoint>();
            RegisterProgressWatchers(spawner.gameObject);

            spawner.SetSpawnData(spawnerData.Id, spawnerData.EnemyTypeId);
            spawner.Initialize(player);

            return spawner;
        }

        public GameObject CreateEmptyObjectWithName(string name) 
            => UnityEngine.Object.Instantiate(new GameObject(name));

        public async Task<T> CreatePoolableObject<T>(Transform parent, bool activeByDefault) where T : IPoolableObject
        {
            if (typeof(T) == typeof(ArmorPiercingProjectile))
            {
                var poolableObject = await CreateProjectileAsync(ProjectileTypeId.AP, Constants.ArmorPiercingProjectileAddress, parent, activeByDefault);
                return poolableObject.GetComponent<T>();
            }

            if (typeof(T) == typeof(HighExplosiveProjectile))
            {
                var poolableObject = await CreateProjectileAsync(ProjectileTypeId.HEX, Constants.HighExplosiveProjectileAddress, parent, activeByDefault);
                return poolableObject.GetComponent<T>();
            }

            throw new InvalidOperationException($"Unsupported type: {typeof(T)}");
        }

        public ObjectPool<T> CreatePool<T>(Transform parent, ObjectPoolStaticData staticData) where T : IPoolableObject 
            => new(staticData.PoolSize, parent, staticData.AutoExpand, this);

        public void CleanupProgressWatchers()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assetProvider.Cleanup();
        }

        private async Task<Projectile> CreateProjectileAsync(ProjectileTypeId projectileTypeId, string address, Transform parent, bool activeByDefault)
        {
            var prefab = await _assetProvider.Load<GameObject>(address);
            var data = _staticData.ForProjectile(projectileTypeId);

            Projectile projectile = _container.InstantiatePrefab(prefab, parent.position, Quaternion.identity, parent).GetComponent<Projectile>();
            projectile.Initialize(data);
            projectile.gameObject.SetActive(activeByDefault);

            return projectile;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            var playerObject = await _assetProvider.Instantiate(prefabPath, at);
            RegisterProgressWatchers(playerObject);

            return playerObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            var playerObject = await _assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(playerObject);

            return playerObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            var gameObject = UnityEngine.Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject playerObject)
        {
            foreach (var progressReader in playerObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }
    }
}
