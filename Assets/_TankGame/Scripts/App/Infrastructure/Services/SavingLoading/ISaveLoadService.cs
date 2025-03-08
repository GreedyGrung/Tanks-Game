using _TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace _TankGame.App.Infrastructure.Services.SavingLoading
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        void DeleteProgress();
    }
}