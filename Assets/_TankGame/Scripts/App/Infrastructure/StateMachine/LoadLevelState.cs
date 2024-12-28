using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.App.CameraLogic;
using TankGame.App.Entities.Interfaces;
using TankGame.App.Environment;
using TankGame.App.Factory;
using TankGame.App.Infrastructure.Services.SpawnersObserver;
using TankGame.App.Infrastructure.Services.StaticData;
using TankGame.App.Infrastructure.Services.UI;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.App.StaticData;
using TankGame.App.UI;
using TankGame.Core.Services.Input;
using TankGame.Core.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.App.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIService _uiService;
        private readonly IUIFactory _uiFactory;
        private readonly ISpawnersObserverService _spawnersObserverService;
        private readonly List<SpawnPoint> _spawnPoints = new();

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingScreen loadingScreen,
            IGameFactory gameFactory,
            IPersistentProgressService progressService,
            IStaticDataService staticData,
            IUIService uIService,
            IUIFactory uiFactory,
            ISpawnersObserverService spawnersObserverService)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
            _uiService = uIService;
            _uiFactory = uiFactory;
            _spawnersObserverService = spawnersObserverService;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.CleanupProgressWatchers();
            _gameFactory.WarmUp();
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private async void OnLoaded()
        {
            await InitGameUIAsync();
            await InitGameWorld();
            InformProgressReaders();
            InitSpawnersObserverService();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private async Task InitGameWorld()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            GameObject inputObject = await _gameFactory.CreateInputAsync();
            UnityActionsInputService input = inputObject.GetComponent<UnityActionsInputService>();

            GameObject playerObject = await _gameFactory.CreatePlayerAsync(levelData.PlayerPosition);
            IPlayer player = playerObject.GetComponent<IPlayer>();
            player.Init(input, _spawnersObserverService);

            GameObject hud = await _gameFactory.CreateHudAsync();
            hud.GetComponent<PlayerStatsPanel>().Init(player);

            var cameraFollow = Object.FindObjectOfType<CameraFollow>();
            cameraFollow.Init(player);

            await InitSpawners(player, levelData);

            UIMediator uiMediator = new(_uiService, player, _spawnersObserverService);
        }

        private async Task InitSpawners(IPlayer player, LevelStaticData levelData)
        {
            foreach (var spawnerData in levelData.EnemySpawners)
            {
                _spawnPoints.Add(await _gameFactory.CreateSpawnerAsync(spawnerData, player));
            }
        }

        private void InitSpawnersObserverService()
        {
            _spawnersObserverService.Init(_spawnPoints);
        }

        private async Task InitGameUIAsync()
        {
            await _uiFactory.CreateUIRootAsync();

            _uiService.ReceivePanels(_uiFactory.CreateUIPanels());
        }

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
            {
                reader.LoadProgress(_progressService.Progress);
            }
        }
    }
}