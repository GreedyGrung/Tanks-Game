using System;
using System.Threading.Tasks;

namespace TankGame.App.Infrastructure.Services.ScenesLoading
{
    public interface ISceneLoader
    {
        Task LoadAsync(string nextScene, Action onLoaded = null);
    }
}