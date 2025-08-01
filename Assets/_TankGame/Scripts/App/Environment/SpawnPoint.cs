using System;
using TankGame.App.Entities.Enemies.Base;
using TankGame.App.Entities.Interfaces;
using TankGame.App.Factory;
using TankGame.App.Infrastructure.Services.PersistentProgress;
using TankGame.App.Infrastructure.Services.PersistentProgress.Data;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Utils.Enums;
using UnityEngine;
using Zenject;

namespace TankGame.App.Environment
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public event Action<SpawnPoint> OnEnemyInSpawnerKilled;

        private string _id;
        private EnemyTypeId _enemyType;
        private IGameFactory _gameFactory;
        private bool _isSlain;
        private IPlayer _player;
        private IPoolsService _poolsService;
        private Enemy _enemy;

        public bool IsSlain => _isSlain;

        [Inject]
        private void Construct(IGameFactory gameFactory, IPoolsService poolsService)
        {
            _gameFactory = gameFactory;
            _poolsService = poolsService;
        }

        public void SetSpawnData(string id, EnemyTypeId enemyType)
        {
            _id = id;
            _enemyType = enemyType;
        }

        public void Initialize(IPlayer player)
        {
            _player = player;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
            {
                _isSlain = true;
            }
            else
            {
                Spawn();
            }
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_isSlain)
            {
                playerProgress.KillData.ClearedSpawners.Add(_id);
            }
        }

        private async void Spawn()
        {
            _enemy = await _gameFactory.CreateEnemyAsync(_enemyType, transform);

            _enemy.OnKilled += KillEnemy;
            _enemy.Initialize(_player, _poolsService);
        }

        private void KillEnemy()
        {
            _enemy.OnKilled -= KillEnemy;
            _isSlain = true;

            OnEnemyInSpawnerKilled?.Invoke(this);
        }
    }
}