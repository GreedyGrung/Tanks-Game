using TankGame.Core.Data;

namespace TankGame.Core.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }
}
