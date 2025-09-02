using GreedyLogger;
using TankGame.Runtime.Factory;
using TankGame.Runtime.Infrastructure.Services.PersistentProgress;
using TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data;
using TankGame.Runtime.Utils;
using UnityEngine;

namespace TankGame.Runtime.Infrastructure.Services.SavingLoading
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

            GLogger.Log("Successfully saved the progress!", LogContext.Infrastructure);
            PlayerPrefs.SetString(Constants.SaveKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
            => PlayerPrefs.GetString(Constants.SaveKey)?.ToDeserizalized<PlayerProgress>();

        public void DeleteProgress() 
            => PlayerPrefs.DeleteAll();
    }
}