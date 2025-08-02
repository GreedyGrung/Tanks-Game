using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.Runtime.CameraLogic;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Environment;
using TankGame.Runtime.Factory;
using TankGame.Runtime.Infrastructure.Services.PersistentProgress;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.Infrastructure.Services.ScenesLoading;
using TankGame.Runtime.Infrastructure.Services.SpawnersObserver;
using TankGame.Runtime.Infrastructure.Services.StaticData;
using TankGame.Runtime.Infrastructure.Services.UI;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using TankGame.Runtime.Projectiles;
using TankGame.Runtime.StaticData.Environment;
using TankGame.Runtime.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.Runtime.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIService _uiService;
        private readonly IUIFactory _uiFactory;
        private readonly ISpawnersObserverService _spawnersObserverService;
        private readonly IPoolsService _poolsService;
        private readonly List<SpawnPoint> _spawnPoints = new();

        private UIMediator _uiMediator;

        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            LoadingScreen loadingScreen,
            IGameFactory gameFactory,
            IPersistentProgressService progressService,
            IStaticDataService staticData,
            IUIService uIService,
            IUIFactory uiFactory,
            ISpawnersObserverService spawnersObserverService,
            IPoolsService poolsService)
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
            _poolsService = poolsService;
        }

        public void Enter(string sceneName)
        {
            _poolsService.Dispose();
            _gameFactory.CleanupProgressWatchers();
            _gameFactory.Dispose();
            _loadingScreen.Show();
            _sceneLoader.LoadAsync(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();

            _spawnPoints.Clear();
        }

        private async void OnLoaded()
        {
            await InitGameUIAsync();
            await InitGameWorldAsync();
            await _gameFactory.LoadProjectiles();
            InitObjectPools();
            InformProgressReaders();
            InitSpawnersObserverService();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private async Task InitGameWorldAsync()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            GameObject playerObject = await _gameFactory.CreatePlayerAsync(levelData.PlayerPosition);
            IPlayer player = playerObject.GetComponent<IPlayer>();

            var hud = await _gameFactory.CreateHudAsync();
            hud.GetComponent<PlayerStatsPanel>().Initialize(player);

            Object.FindObjectOfType<CameraFollow>().Initialize(player.Transform);

            await InitSpawnersAsync(player, levelData);

            _uiMediator = new(_uiService, player, _spawnersObserverService);
        }

        private void InitObjectPools()
        {
            _poolsService.RegisterPool<ArmorPiercingProjectile>();
            _poolsService.RegisterPool<HighExplosiveProjectile>();
        }

        private async Task InitSpawnersAsync(IPlayer player, LevelStaticData levelData)
        {
            var spawnersRoot = _gameFactory.CreateEmptyObjectWithName("Spawners Root");

            foreach (var spawnerData in levelData.EnemySpawners)
            {
                _spawnPoints.Add(await _gameFactory.CreateSpawnerAsync(spawnerData, player, spawnersRoot.transform));
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