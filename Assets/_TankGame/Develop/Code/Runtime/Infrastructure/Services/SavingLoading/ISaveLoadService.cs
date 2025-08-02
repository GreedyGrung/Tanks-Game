using TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.Runtime.Infrastructure.Services.SavingLoading
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        void DeleteProgress();
    }
}