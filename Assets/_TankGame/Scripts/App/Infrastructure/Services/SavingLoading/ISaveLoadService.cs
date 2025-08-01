using TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.App.Infrastructure.Services.SavingLoading
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        void DeleteProgress();
    }
}