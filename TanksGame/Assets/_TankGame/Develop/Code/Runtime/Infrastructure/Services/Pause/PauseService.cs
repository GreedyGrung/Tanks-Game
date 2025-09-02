using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Runtime.Infrastructure.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly List<IPausable> _pausables = new();
        
        public bool IsPaused { get; private set; }
        
        public void Register(IPausable handler) => _pausables.Add(handler);

        public void Unregister(IPausable handler) => _pausables.Remove(handler);

        public void TogglePause()
        {
            IsPaused = !IsPaused;
            
            foreach (var handler in _pausables)
            {
                handler.SetIsPaused(IsPaused);
            }
        }

        public void Dispose() => _pausables.Clear();
    }
}