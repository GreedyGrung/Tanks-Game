using Assets.Scripts.Data;
using Assets.Scripts.Factory;
using TankGame.Core.Utils;
using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string TestSaveKey = "testSave";

    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;

    public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService)
    {
        _gameFactory = gameFactory;
        _progressService = progressService;
    }

    public void SaveProgress()
    {
        foreach (var writer in _gameFactory.ProgressWriters)
        {
            writer.UpdateProgress(_progressService.Progress);
        }

        Debug.Log("saved at " + TestSaveKey);
        PlayerPrefs.SetString(TestSaveKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress()
    {
        Debug.Log("SERVICE: " + PlayerPrefs.GetString(TestSaveKey));
        return PlayerPrefs.GetString(TestSaveKey)?.ToDeserizalized<PlayerProgress>();
    }
}