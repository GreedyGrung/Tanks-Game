using Assets.Scripts.Factory;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : IPayloadedState<string>
{
    public static event Action<Player> OnPlayerSpawned;

    private const string PlayerSpawnTag = "PlayerSpawnPoint";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;

    public LoadLevelState(
        GameStateMachine gameStateMachine, 
        SceneLoader sceneLoader, 
        LoadingScreen loadingScreen, 
        IGameFactory gameFactory, 
        IPersistentProgressService progressService,
        IStaticDataService staticData)
    {
        _sceneLoader = sceneLoader;
        _loadingScreen = loadingScreen;
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _staticData = staticData;
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
        InitGameWorld();
        InformProgressReaders();

        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
        var initialPoint = GameObject.FindGameObjectWithTag(PlayerSpawnTag);

        UnityActionsInputService input = _gameFactory.CreateInput().GetComponent<UnityActionsInputService>();
        Player player = _gameFactory.CreatePlayer(initialPoint).GetComponent<Player>();
        player.Init(input);
        _gameFactory.CreateHud().GetComponent<PlayerStatsPanel>().Init(player);

        InitSpawners(player);

        OnPlayerSpawned?.Invoke(player);
    }

    private void InitSpawners(Player player)
    {
        string sceneKey = SceneManager.GetActiveScene().name;
        LevelStaticData levelData = _staticData.ForLevel(sceneKey);

        foreach (var spawnerData in levelData.EnemySpawners)
        {
            _gameFactory.CreateSpawner(spawnerData, player);
        }

        var enemiesController = GameObject.FindObjectOfType<EnemiesController>();
        enemiesController.Init();
    }

    private void InformProgressReaders()
    {
        foreach (var reader in _gameFactory.ProgressReaders)
        {
            reader.LoadProgress(_progressService.Progress);
        }
    }
}