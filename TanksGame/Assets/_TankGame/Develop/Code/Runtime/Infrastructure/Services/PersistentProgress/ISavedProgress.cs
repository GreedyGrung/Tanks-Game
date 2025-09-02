using TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.Runtime.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress playerProgress);
    }
}
