using Assets.Scripts.Services.AssetManagement;
using Assets.Scripts.Services.PersistentProgress;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(GameObject at) 
            => InstantiateRegistered(Constants.PlayerPath, at.transform.position);

        public GameObject CreateInput() 
            => InstantiateRegistered(Constants.UnityInputActionsPath);

        public GameObject CreateHud() 
            => InstantiateRegistered(Constants.HudPath);

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
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
    }
}
