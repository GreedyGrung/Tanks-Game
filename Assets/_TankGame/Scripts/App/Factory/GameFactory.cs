using Assets.Scripts.Services.AssetManagement;
using Assets.Scripts.Services.PersistentProgress;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(Constants.SpawnerAddress);
        }

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

            var enemy = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent).GetComponent<Enemy>();
            enemy.SetData(enemyData);

            return enemy;
        }

        public async Task<SpawnPoint> CreateSpawnerAsync(EnemySpawnerData spawnerData, Player player)
        {
            var prefab = await _assetProvider.Load<GameObject>(Constants.SpawnerAddress);
            var spawner = InstantiateRegistered(prefab, spawnerData.Position).GetComponent<SpawnPoint>();
            
            spawner.Construct(this);
            spawner.SetSpawnData(spawnerData.Id, spawnerData.EnemyTypeId, spawnerData.IsRandom);
            spawner.InitPlayer(player);

            return spawner;
        }

        public void CleanupProgressWatchers()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assetProvider.Cleanup();
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
            var gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
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
