using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.Runtime.CameraLogic;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Environment;
using TankGame.Runtime.Factory;
using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.Infrastructure.Services.Pause;
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
using TankGame.Runtime.UI.Panels;
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
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;
        private readonly List<SpawnPoint> _spawnPoints = new();

        private UIMediator _uiMediator;
        private IPlayer _player;

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
            IPoolsService poolsService, 
            IInputService inputService, 
            IPauseService pauseService)
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
            _inputService = inputService;
            _pauseService = pauseService;
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

            _gameStateMachine.Enter<GameLoopState, GameLoopPayload>(new GameLoopPayload { Player = _player });
        }

        private async Task InitGameWorldAsync()
        {
            var levelData = LoadLevelData();
            var player = await CreatePlayer(levelData);

            _player = player;
            await CreateCamera(_player);
            _player.Initalize();
            await CreateHud(_player);
            await InitSpawnersAsync(_player, levelData);

            _uiMediator = new(_uiService, _player, _spawnersObserverService, _inputService, _pauseService);
        }

        private async Task CreateCamera(IPlayer player)
        {
            var camera = await _gameFactory.CreateCameraAsync();
            camera.GetComponent<CameraFollow>().Initialize(player.Transform);
        }

        private async Task<IPlayer> CreatePlayer(LevelStaticData levelData)
        {
            GameObject playerObject = await _gameFactory.CreatePlayerAsync(levelData.PlayerPosition);
            IPlayer player = playerObject.GetComponent<IPlayer>();
            
            return player;
        }

        private LevelStaticData LoadLevelData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            
            return levelData;
        }

        private async Task CreateHud(IPlayer player)
        {
            var hud = await _gameFactory.CreateHudAsync();
            hud.GetComponent<PlayerStatsPanel>().Initialize(player);
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