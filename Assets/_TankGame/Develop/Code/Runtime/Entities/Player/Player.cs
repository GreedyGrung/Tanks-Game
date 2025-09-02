using System;
using TankGame.Runtime.Utils;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.Infrastructure.Services.Pause;
using TankGame.Runtime.Infrastructure.Services.PersistentProgress;
using TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.Infrastructure.Services.SpawnersObserver;
using TankGame.Runtime.StaticData.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TankGame.Runtime.Entities.Player
{
    [SelectionBase]
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour, IDamageable, ISavedProgress, IPlayer, IPausable
    {
        [SerializeField] private PlayerWeapon _weapon;
        [SerializeField] private PlayerHealthData _healthData;

        private PlayerMovement _playerMovement;
        private ISpawnersObserverService _spawnersObserverService;
        private IInputService _inputService;
        private IPoolsService _poolsService;

        private string CurrentLevel => SceneManager.GetActiveScene().name;

        public IHealth Health { get; private set; }
        public PlayerWeapon Weapon => _weapon;
        public Transform Transform => transform;

        public bool IsPaused { get; private set; }

        private void Update()
        {
            if (IsPaused || _playerMovement == null)
            {
                return;
            }
            
            _playerMovement.LogicUpdate();
            _weapon.LogicUpdate();
        }

        private void FixedUpdate()
        {
            if (IsPaused || _playerMovement == null)
            {
                return;
            }
            
            _playerMovement.PhysicsUpdate();
        }

        private void OnDestroy() => Unsubscribe();

        public void Initalize()
        {
            Health = new PlayerHealth(_healthData.MaxHealth, _healthData.MaxHealth);
            _playerMovement = GetComponent<PlayerMovement>();
            _playerMovement.Init(_inputService);
            _weapon.Init(this, _inputService, _poolsService);

            Subscribe();
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

        public void TakeDamage(float damage) => Health.Subtract(damage);

        public void SetIsPaused(bool value) => IsPaused = value;

        [Inject]
        private void Construct(IInputService inputService, ISpawnersObserverService spawnersObserverService, IPoolsService poolsService)
        {
            _poolsService = poolsService;
            _inputService = inputService;
            _spawnersObserverService = spawnersObserverService;
        }

        private void Subscribe()
        {
            _spawnersObserverService.OnAllEnemiesKilled += DeactivatePlayer;
            Health.OnDied += DeactivatePlayer;
        }

        private void Unsubscribe()
        {
            _spawnersObserverService.OnAllEnemiesKilled -= DeactivatePlayer;
            Health.OnDied -= DeactivatePlayer;
        }

        private void DeactivatePlayer()
        {
            _playerMovement.enabled = false;
            _weapon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}