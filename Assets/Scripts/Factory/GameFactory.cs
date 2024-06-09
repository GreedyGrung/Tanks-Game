using Assets.Scripts.Services.AssetManagement;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

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
    }
}
