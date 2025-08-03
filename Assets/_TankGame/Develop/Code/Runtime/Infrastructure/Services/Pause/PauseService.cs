using System.Collections.Generic;

namespace TankGame.Runtime.Infrastructure.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly List<IPausable> _pausables = new();
        
        public bool IsPaused { get; private set; }
        
        public void Register(IPausable handler) => _pausables.Add(handler);

        public void UnRegister(IPausable handler) => _pausables.Remove(handler);

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            
            foreach (var handler in _pausables)
            {
                handler.SetIsPaused(isPaused);
            }
        }
    }
}