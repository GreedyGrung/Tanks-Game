using TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.Runtime.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }
}
