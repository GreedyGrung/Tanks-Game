using Assets.Scripts.Services.AssetManagement;
using Assets.Scripts.Services.PersistentProgress;
using System.Collections.Generic;
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

        public GameObject CreatePlayer(Vector3 at) 
            => InstantiateRegistered(Constants.PlayerPath, at);

        public GameObject CreateInput() 
            => InstantiateRegistered(Constants.UnityInputActionsPath);

        public GameObject CreateHud() 
            => InstantiateRegistered(Constants.HudPath);

        public Enemy CreateEnemy(EnemyTypeId type, Transform parent)
        {
            var enemyData = _staticData.ForEnemy(type);
            var enemy = Object.Instantiate(enemyData.EnemyPrefab, parent.position, Quaternion.identity, parent);
            enemy.SetData(enemyData);

            return enemy;
        }

        public SpawnPoint CreateSpawner(EnemySpawnerData spawnerData, Player player)
        {
            var spanwer = InstantiateRegistered(Constants.SpawnerPath, spawnerData.Position).GetComponent<SpawnPoint>();
            
            spanwer.Construct(this);
            spanwer.SetSpawnData(spawnerData.Id, spawnerData.EnemyTypeId, spawnerData.IsRandom);
            spanwer.InitPlayer(player);

            return spanwer;
        }

        public void CleanupProgressWatchers()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
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
