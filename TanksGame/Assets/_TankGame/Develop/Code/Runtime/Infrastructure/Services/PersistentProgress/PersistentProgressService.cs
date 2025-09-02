using TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.Runtime.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
