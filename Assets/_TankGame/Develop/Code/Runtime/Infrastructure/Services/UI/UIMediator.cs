using System;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.Infrastructure.Services.Pause;
using TankGame.Runtime.Infrastructure.Services.SpawnersObserver;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Infrastructure.Services.UI
{
    public class UIMediator : IDisposable
    {
        private readonly IUIService _uiService;
        private readonly IPlayer _player;
        private readonly ISpawnersObserverService _spawnersObserverService;
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;

        public UIMediator(IUIService uiService, IPlayer player, ISpawnersObserverService spawnersObserverService, IInputService inputService, IPauseService pauseService)
        {
            _uiService = uiService;
            _player = player;
            _spawnersObserverService = spawnersObserverService;
            _inputService = inputService;
            _pauseService = pauseService;

            _player.Health.OnDied += OnPlayerDied;
            _spawnersObserverService.OnAllEnemiesKilled += OnAllEnemiesKilled;
            _inputService.OnPausePressed += OnPausePressed;
        }
        
        private void OnPlayerDied() => _uiService.Open(UIPanelId.FailurePanel);

        private void OnAllEnemiesKilled() => _uiService.Open(UIPanelId.VictoryPanel);

        private void OnPausePressed()
        {
            if (!_pauseService.IsPaused)
            {
                _uiService.Open(UIPanelId.PausePanel);
            }
            else
            {
                _uiService.Close(UIPanelId.PausePanel);
            }
        }

        public void Dispose()
        {
            _player.Health.OnDied -= OnPlayerDied;
            _spawnersObserverService.OnAllEnemiesKilled -= OnAllEnemiesKilled;
            _inputService.OnPausePressed -= OnPausePressed;
        }
    }
}
