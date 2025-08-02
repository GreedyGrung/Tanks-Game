using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TankGame.Runtime.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public void Initialize() 
            => Addressables.InitializeAsync();

        public Task<GameObject> Instantiate(string address)
            => Addressables.InstantiateAsync(address).Task;

        public Task<GameObject> Instantiate(string address, Vector3 at) 
            => Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out var completedHandle))
            {
                return completedHandle.Result as T;
            }

            var operationToRun = Addressables.LoadAssetAsync<T>(assetReference);

            return await RunWithCacheOnComplete(operationToRun, cacheKey: assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out var completedHandle))
            {
                return completedHandle.Result as T;
            }

            var operationToRun = Addressables.LoadAssetAsync<T>(address);

            return await RunWithCacheOnComplete(operationToRun, cacheKey: address);
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

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += newHandle => _completedCache[cacheKey] = newHandle;

            AddHandle(cacheKey, handle);

            return await handle.Task;
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
