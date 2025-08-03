namespace TankGame.Runtime.Infrastructure.Services.Pause
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void Register(IPausable handler);
        void UnRegister(IPausable handler);
        void SetPaused(bool isPaused);
    }
}