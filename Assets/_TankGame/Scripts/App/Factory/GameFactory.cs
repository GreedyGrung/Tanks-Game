using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _TankGame.App.Entities.Enemies.Base;
using _TankGame.App.Entities.Interfaces;
using _TankGame.App.Environment;
using _TankGame.App.Infrastructure.Services.AssetManagement;
using _TankGame.App.Infrastructure.Services.PersistentProgress;
using _TankGame.App.Infrastructure.Services.PoolsService;
using _TankGame.App.Infrastructure.Services.StaticData;
using _TankGame.App.Projectiles;
using _TankGame.App.StaticData.Enemies;
using _TankGame.App.StaticData.Environment;
using _TankGame.App.Utils;
using _TankGame.App.Utils.Enums;
using UnityEngine;
using Zenject;

namespace _TankGame.App.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;

        private Dictionary<ProjectileTypeId, GameObject> _projectiles;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, DiContainer container)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _container = container;
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public async Task WarmUp()
        {
            _projectiles = new();
            await _assetProvider.Load<GameObject>(Constants.SpawnerAddress);
        }

        public async Task<GameObject> CreatePlayerAsync(Vector3 at)
            => await InstantiateRegisteredAsync(Constants.PlayerAddress, at);

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

        public T CreatePoolableObject<T>(Transform parent, bool activeByDefault) where T : IPoolableObject
        {
            if (typeof(T) == typeof(ArmorPiercingProjectile))
            {
                var poolableObject = CreateProjectile(ProjectileTypeId.AP, parent, activeByDefault);
                return poolableObject.GetComponent<T>();
            }

            if (typeof(T) == typeof(HighExplosiveProjectile))
            {
                var poolableObject = CreateProjectile(ProjectileTypeId.HEX, parent, activeByDefault);
                return poolableObject.GetComponent<T>();
            }

            throw new InvalidOperationException($"Unsupported type: {typeof(T)}");
        }

        public ObjectPool<T> CreatePool<T>(Transform parent, ObjectPoolStaticData staticData) where T : IPoolableObject 
            => new(staticData.PoolSize, parent, staticData.AutoExpand, this);

        public async Task LoadProjectiles()
        {
            await LoadProjectileFromAssetProvider(ProjectileTypeId.AP, Constants.ArmorPiercingProjectileAddress);
            await LoadProjectileFromAssetProvider(ProjectileTypeId.HEX, Constants.HighExplosiveProjectileAddress);
        }

        public void CleanupProgressWatchers()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assetProvider.Cleanup();
        }

        public void Dispose()
        {
            _projectiles?.Clear();
        }

        private async Task LoadProjectileFromAssetProvider(ProjectileTypeId id, string address)
        {
            var projectile = await _assetProvider.Load<GameObject>(address);

            if (_projectiles.ContainsKey(id))
            {
                Debug.LogError($"Dictionary already contains key {id}! Adding operation will be skipped!");
                return;
            }

            _projectiles.Add(id, projectile);
        }

        private Projectile CreateProjectile(ProjectileTypeId projectileTypeId, Transform parent, bool activeByDefault)
        {
            var data = _staticData.ForProjectile(projectileTypeId);
            var prefab = _projectiles[projectileTypeId];

            Projectile projectile = _container.InstantiatePrefab(prefab, parent.position, Quaternion.identity, parent).GetComponent<Projectile>();
            projectile.Initialize(data);
            projectile.gameObject.SetActive(activeByDefault);

            return projectile;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            var playerObject = await _assetProvider.Instantiate(prefabPath, at);
            RegisterProgressWatchers(playerObject);
            _container.InjectGameObject(playerObject);

            return playerObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            var playerObject = await _assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(playerObject);

            return playerObject;
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
