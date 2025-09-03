using System.IO;
using System.Text;
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
        private readonly string _path;

        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        
        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;

            _path = Path.Combine(Application.persistentDataPath, Constants.SaveKey);
        }

        public void SaveProgress()
        {
            foreach (var writer in _gameFactory.ProgressWriters)
            {
                writer.UpdateProgress(_progressService.Progress);
            }

            string json = _progressService.Progress.ToJson();
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            WriteAtomic(_path, bytes);
            
            GLogger.Log($"Progress saved to: {_path}", LogContext.Infrastructure);
        }

        public PlayerProgress LoadProgress()
        {
            if (!File.Exists(_path)) return null;
            
            byte[] bytes = File.ReadAllBytes(_path);
            string json = Encoding.UTF8.GetString(bytes);
                    
            return json.ToDeserizalized<PlayerProgress>();
        }

        public void DeleteProgress()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }

        private void WriteAtomic(string path, byte[] bytes)
        {
            string dir = Path.GetDirectoryName(path);
            
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string tmp = path + ".tmp";
            File.WriteAllBytes(tmp, bytes);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.Move(tmp, path);
        }
    }
}