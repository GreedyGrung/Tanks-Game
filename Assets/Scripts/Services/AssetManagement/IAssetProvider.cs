using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string resourcePath);
        GameObject Instantiate(string resourcePath, Vector3 at);
    }
}