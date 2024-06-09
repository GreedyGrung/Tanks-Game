using Assets.Scripts.Factory;
using System;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    public static event Action<Player> OnPlayerSpawned;

    private const string PlayerSpawnTag = "PlayerSpawnPoint";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory)
    {
        _sceneLoader = sceneLoader;
        _loadingScreen = loadingScreen;
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
    }

    public void Enter(string sceneName)
    {
        _loadingScreen.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
        _loadingScreen.Hide();
    }

    private void OnLoaded()
    {
        var initialPoint = GameObject.FindGameObjectWithTag(PlayerSpawnTag);

        UnityActionsInputService input = _gameFactory.CreateInput().GetComponent<UnityActionsInputService>();
        Player player = _gameFactory.CreatePlayer(initialPoint).GetComponent<Player>();
        player.Init(input);
        _gameFactory.CreateHud();

        OnPlayerSpawned?.Invoke(player);

        _gameStateMachine.Enter<GameLoopState>();
    }
}