using Assets.Scripts.Factory;
using System;
using System.Collections.Generic;
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
        _loadingScreen.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
        _loadingScreen.Hide();
    }

    private void OnLoaded()
    {
        InitGameUI();
        InitGameWorld();
        InformProgressReaders();
        InitSpawnersObserverService();

        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
        string sceneKey = SceneManager.GetActiveScene().name;
        LevelStaticData levelData = _staticData.ForLevel(sceneKey);

        UnityActionsInputService input = _gameFactory.CreateInput().GetComponent<UnityActionsInputService>();
        Player player = _gameFactory.CreatePlayer(levelData.PlayerPosition).GetComponent<Player>();
        player.Init(input, _uiService, _spawnersObserverService);
        _gameFactory.CreateHud().GetComponent<PlayerStatsPanel>().Init(player);

        InitSpawners(player, levelData);

        OnPlayerSpawned?.Invoke(player);
    }

    private void InitSpawners(Player player, LevelStaticData levelData)
    {
        foreach (var spawnerData in levelData.EnemySpawners)
        {
            _spawnPoints.Add(_gameFactory.CreateSpawner(spawnerData, player));
        }
    }

    private void InitSpawnersObserverService()
    {
        _spawnersObserverService.Init(_spawnPoints);
    }

    private void InitGameUI()
    {
        _uiFactory.CreateUIRoot();
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