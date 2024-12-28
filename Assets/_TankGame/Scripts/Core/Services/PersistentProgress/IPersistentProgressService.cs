using TankGame.Core.Data;

namespace TankGame.Core.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}