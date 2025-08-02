using TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.Runtime.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}