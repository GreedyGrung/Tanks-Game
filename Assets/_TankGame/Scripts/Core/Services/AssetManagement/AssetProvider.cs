using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

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

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out var completedHandle))
            {
                return completedHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(assetReference);
            handle.Completed += newHandle =>
            {
                _completedCache[assetReference.AssetGUID] = newHandle;
            };

            AddHandle(assetReference.AssetGUID, handle);

            return await handle.Task;
        }

        public void Cleanup()
        {
            foreach (var resourceHandles in _handles.Values)
            {
                foreach (var handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }

            _completedCache.Clear();
            _handles.Clear();
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out var resourceHandle))
            {
                resourceHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandle;
            }

            resourceHandle.Add(handle);
        }
    }
}
