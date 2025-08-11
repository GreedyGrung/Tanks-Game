namespace TankGame.Runtime.Infrastructure.Services.Pause
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void Register(IPausable handler);
        void Unregister(IPausable handler);
        void TogglePause();
        void Dispose();
    }
}