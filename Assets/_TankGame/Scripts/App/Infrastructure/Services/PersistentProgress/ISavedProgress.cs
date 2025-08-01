using TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.App.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress playerProgress);
    }
}
