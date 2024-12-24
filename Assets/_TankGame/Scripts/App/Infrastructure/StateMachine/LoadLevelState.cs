using Assets.Scripts.Factory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : IPayloadedState<string>
{
    public static event Action<Player> OnPlayerSpawned;

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIService _uiService;
    private readonly IUIFactory _uiFactory;
    private readonly ISpawnersObserverService _spawnersObserverService;
    private List<SpawnPoint> _spawnPoints = new();

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
        Player player = playerObject.GetComponent<Player>();
        player.Init(input, _uiService, _spawnersObserverService);

        GameObject hud = await _gameFactory.CreateHudAsync();
        hud.GetComponent<PlayerStatsPanel>().Init(player);

        await InitSpawners(player, levelData);

        UIMediator uiMediator = new(_uiService, player, _spawnersObserverService);

        OnPlayerSpawned?.Invoke(player);
    }

    private async Task InitSpawners(Player player, LevelStaticData levelData)
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