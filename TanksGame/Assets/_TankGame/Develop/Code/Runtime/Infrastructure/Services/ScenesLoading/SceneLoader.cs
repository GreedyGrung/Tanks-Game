using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TankGame.Runtime.Infrastructure.Services.ScenesLoading
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadAsync(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                
                return;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                await UniTask.Yield();
            }

            onLoaded?.Invoke();
        }
    }
}
