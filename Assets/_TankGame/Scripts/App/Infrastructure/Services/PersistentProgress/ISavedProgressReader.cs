using _TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace _TankGame.App.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }
}
