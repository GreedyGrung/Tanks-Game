using _TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace _TankGame.App.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress playerProgress);
    }
}
