using System;
using _TankGame.App.Entities.Interfaces;
using _TankGame.App.Infrastructure.Services.SpawnersObserver;
using _TankGame.App.Infrastructure.Services.UI;
using _TankGame.App.Utils.Enums;

namespace _TankGame.App.Infrastructure
{
    public class UIMediator : IDisposable
    {
        private readonly IUIService _uiService;
        private readonly IPlayer _player;
        private readonly ISpawnersObserverService _spawnersObserverService;

        public UIMediator(IUIService uiService, IPlayer player, ISpawnersObserverService spawnersObserverService)
        {
            _uiService = uiService;
            _player = player;
            _spawnersObserverService = spawnersObserverService;

            player.Health.OnDied += OnPlayerDied;
            spawnersObserverService.OnAllEnemiesKilled += OnAllEnemiesKilled;
        }

        private void OnPlayerDied() => _uiService.Open(UIPanelId.FailurePanel);

        private void OnAllEnemiesKilled() => _uiService.Open(UIPanelId.VictoryPanel);

        public void Dispose()
        {
            _player.Health.OnDied -= OnPlayerDied;
            _spawnersObserverService.OnAllEnemiesKilled -= OnAllEnemiesKilled;
        }
    }
}
