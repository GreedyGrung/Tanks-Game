using Assets.Scripts.Infrastructure;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string resourcePath);
        GameObject Instantiate(string resourcePath, Vector3 at);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void Cleanup();
    }
}