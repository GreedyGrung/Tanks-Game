using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TankGame.App.Infrastructure.Services.ScenesLoading
{
    public class SceneLoader : ISceneLoader
    {
        public async Task LoadAsync(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                
                return;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                await Task.Yield();
            }

            onLoaded?.Invoke();
        }
    }
}
