using TankGame.Core.Data;
using TankGame.Core.Services;

namespace TankGame.App.Infrastructure.Services.SavingLoading
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}