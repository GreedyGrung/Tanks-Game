using _TankGame.App.Infrastructure.Services.PersistentProgress;
using _TankGame.App.Infrastructure.Services.PersistentProgress.Data;
using _TankGame.App.Infrastructure.Services.SavingLoading;
using UnityEngine;
using Zenject;

namespace _TankGame.App.Utils
{
    public class TestProgressSaver : MonoBehaviour, ISavedProgress
    {
        private const int MinValue = 0;
        private const int MaxValue = 1000;

        private int RandomValue => Random.Range(MinValue, MaxValue);
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _saveLoadService.SaveProgress();
            }
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            Debug.Log("LOADED: " + playerProgress.TestValue);
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.TestValue = RandomValue;
            Debug.Log("SAVED: " + playerProgress.TestValue);
        }
    }
}
