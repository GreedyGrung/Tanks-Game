using TankGame.App.Entities.Interfaces;
using TankGame.App.Entities.Player.Data;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Infrastructure.Services.SpawnersObserver;
using TankGame.App.Interfaces;
using TankGame.Core.Data;
using TankGame.Core.Services.Input;
using TankGame.Core.Services.PersistentProgress;
using TankGame.Core.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.App.Entities.Player
{
    [SelectionBase]
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour, IDamageable, ISavedProgress, IPlayer
    {
        [SerializeField] private PlayerWeapon _weapon;
        [SerializeField] private PlayerHealthData _healthData;

        private PlayerMovement _playerMovement;
        private ISpawnersObserverService _spawnersObserverService;

        private string CurrentLevel => SceneManager.GetActiveScene().name;

        public IHealth Health { get; private set; }
        public PlayerWeapon Weapon => _weapon;
        public Transform Transform => transform;

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _spawnersObserverService.OnAllEnemiesKilled -= DeactivatePlayer;
            Health.OnDied -= DeactivatePlayer;
        }

        public void Init(IInputService inputService, ISpawnersObserverService spawnersObserverService, IPoolsService poolsService)
        {
            Health = new PlayerHealth(_healthData.MaxHealth, _healthData.MaxHealth);
            _playerMovement = GetComponent<PlayerMovement>();
            _playerMovement.Init(inputService);
            _weapon.Init(inputService, poolsService);
            _spawnersObserverService = spawnersObserverService;

            Subscribe();
        }

        private void Subscribe()
        {
            _spawnersObserverService.OnAllEnemiesKilled += DeactivatePlayer;
            Health.OnDied += DeactivatePlayer;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.PlayerData.Health = Health.Value;
            playerProgress.PlayerData.PositionOnLevel = new(CurrentLevel, transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (CurrentLevel == playerProgress.PlayerData.PositionOnLevel.Level
                && playerProgress.PlayerData.PositionOnLevel.Position != null)
            {
                var savedPosition = playerProgress.PlayerData.PositionOnLevel.Position;
                transform.position = savedPosition.AsUnityVector3();
            }

            Health.SetValue(playerProgress.PlayerData.Health != 0 ? playerProgress.PlayerData.Health : Health.MaxValue);
        }

        public void TakeDamage(float damage)
        {
            Health.Subtract(damage);
        }

        private void DeactivatePlayer()
        {
            _playerMovement.enabled = false;
            _weapon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}