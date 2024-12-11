using Assets.Scripts.Data;
using Assets.Scripts.Factory;
using Assets.Scripts.Services.PersistentProgress;
using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour, ISavedProgress
{
    public event Action<SpawnPoint> OnEnemyInSpawnerKilled;

    private string _id;
    private EnemyTypeId _enemyType;
    private IGameFactory _gameFactory;
    private bool _IsSlain;
    private Player _player;

    public Enemy Enemy { get; private set; }

    public void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void SetSpawnData(string id, EnemyTypeId enemyType)
    {
        _id = id;
        _enemyType = enemyType;
    }

    public void InitPlayer(Player player)
    {
        _player = player;
    }

    public void LoadProgress(PlayerProgress playerProgress)
    {
        if (playerProgress.KillData.ClearedSpawners.Contains(_id))
        {
            _IsSlain = true;
        }
        else
        {
            Spawn();
        }
    }

    public void UpdateProgress(PlayerProgress playerProgress)
    {
        if (_IsSlain)
        {
            playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }

    private void Spawn()
    {
        // TODO: Implement it later
        /*if (_isRandom)
        {
            SpawnRandom();

            return;
        }*/

        Enemy = _gameFactory.CreateEnemy(_enemyType, transform);
        Enemy.OnKilled += KillEnemy;

        Enemy.Init(_player);
    }

    private void KillEnemy()
    {
        Enemy.OnKilled -= KillEnemy;
        _IsSlain = true;
        Debug.LogError(gameObject.name + " slain!");

        OnEnemyInSpawnerKilled?.Invoke(this);
    }

    private void SpawnRandom()
    {
        int enemyType = UnityEngine.Random.Range(0, 2);
        Enemy = enemyType == 0 
            ? _gameFactory.CreateEnemy(EnemyTypeId.Tank, transform) 
            : _gameFactory.CreateEnemy(EnemyTypeId.Turret, transform);
        Enemy.Init(_player);
    }
}
