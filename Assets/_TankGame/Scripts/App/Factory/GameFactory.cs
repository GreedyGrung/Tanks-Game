using Assets.Scripts.Services.AssetManagement;
using Assets.Scripts.Services.PersistentProgress;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

        public GameObject CreatePlayer(Vector3 at) 
            => InstantiateRegistered(Constants.PlayerPath, at);

        public GameObject CreateInput() 
            => InstantiateRegistered(Constants.UnityInputActionsPath);

        public GameObject CreateHud() 
            => InstantiateRegistered(Constants.HudPath);

        public async Task<Enemy> CreateEnemy(EnemyTypeId type, Transform parent)
        {
            var enemyData = _staticData.ForEnemy(type);
            GameObject prefab = await _assetProvider.Load<GameObject>(enemyData.PrefabReference);

            var enemy = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent).GetComponent<Enemy>();
            enemy.SetData(enemyData);

            return enemy;
        }

        public async Task<SpawnPoint> CreateSpawner(EnemySpawnerData spawnerData, Player player)
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

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            var playerObject = _assetProvider.Instantiate(prefabPath, at);
            RegisterProgressWatchers(playerObject);

            return playerObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            var playerObject = _assetProvider.Instantiate(prefabPath);
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
