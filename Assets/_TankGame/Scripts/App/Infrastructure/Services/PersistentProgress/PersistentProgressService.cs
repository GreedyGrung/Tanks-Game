using _TankGame.App.Infrastructure.Services.PersistentProgress.Data;

namespace _TankGame.App.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
