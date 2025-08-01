using TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.App.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}