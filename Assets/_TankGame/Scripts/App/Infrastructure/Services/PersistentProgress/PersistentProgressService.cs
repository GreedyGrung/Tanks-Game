using TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace TankGame.App.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
