using TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.App.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }
}
