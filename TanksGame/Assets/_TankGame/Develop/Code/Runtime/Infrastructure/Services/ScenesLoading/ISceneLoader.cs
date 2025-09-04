using System;
using Cysharp.Threading.Tasks;

namespace TankGame.Runtime.Infrastructure.Services.ScenesLoading
{
    public interface ISceneLoader
    {
        UniTask LoadAsync(string nextScene, Action onLoaded = null);
    }
}