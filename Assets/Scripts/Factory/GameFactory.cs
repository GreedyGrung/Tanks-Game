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
            => _assetProvider.Instantiate(Constants.PlayerPath, at.transform.position);

        public GameObject CreateInput() 
            => _assetProvider.Instantiate(Constants.UnityInputActionsPath);

        public GameObject CreateHud() 
            => _assetProvider.Instantiate(Constants.HudPath);

        public void FindProgressWatchers()
        {
            var readers = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISavedProgressReader>();
            var writers = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISavedProgress>();

            ProgressReaders.AddRange(readers);
            ProgressWriters.AddRange(writers);
        }

        public void CleanupProgressWatchers()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}
