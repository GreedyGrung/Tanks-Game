using _TankGame.App.Factory;
using _TankGame.App.Infrastructure.Services.PersistentProgress;
using _TankGame.App.Infrastructure.Services.PersistentProgress.Data;
using _TankGame.App.Utils;
using UnityEngine;

namespace _TankGame.App.Infrastructure.Services.SavingLoading
{
    public class SaveLoadService : ISaveLoadService
    {
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

            Debug.Log("saved at " + Constants.SaveKey);
            PlayerPrefs.SetString(Constants.SaveKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
            => PlayerPrefs.GetString(Constants.SaveKey)?.ToDeserizalized<PlayerProgress>();

        public void DeleteProgress() 
            => PlayerPrefs.DeleteAll();
    }
}