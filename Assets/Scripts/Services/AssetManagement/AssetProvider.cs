using UnityEngine;

namespace Assets.Scripts.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string resourcePath)
        {
            var prefab = Resources.Load<GameObject>(resourcePath);

            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string resourcePath, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(resourcePath);

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}
