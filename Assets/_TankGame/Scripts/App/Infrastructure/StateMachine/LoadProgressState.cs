using Assets.Scripts.Data;

public class LoadProgressState : IState
{
    private const string GameSceneName = "GameScene";

    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        LoadOrInitProgress();
        _gameStateMachine.Enter<LoadLevelState, string>(GameSceneName);
    }

    public void Exit()
    {
        
    }

    private void LoadOrInitProgress()
    {
        _progressService.Progress = _saveLoadService.LoadProgress() ?? InitNewProgress();
    }

    private PlayerProgress InitNewProgress() => new();
}