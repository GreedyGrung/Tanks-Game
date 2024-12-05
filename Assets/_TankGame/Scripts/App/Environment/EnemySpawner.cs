using Assets.Scripts.Data;
using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.PersistentProgress;
using TankGame.Scripts.App.Utils;
using UnityEngine;

[RequireComponent(typeof(UniqueId))]
public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    [SerializeField] private EnemyTypeId _enemyType;
    [SerializeField] private bool _isRandom;

    private string _id;
    private IGameFactory _gameFactory;
    private bool _IsSlain;
    private Player _player;

    public Enemy Enemy { get; private set; }

    private void Awake()
    {
        _id = GetComponent<UniqueId>().Id;

        _gameFactory = ServiceLocator.Instance.Single<IGameFactory>();
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
        if (_isRandom)
        {
            SpawnRandom();

            return;
        }

        Enemy = _gameFactory.CreateEnemy(_enemyType, transform);

        Enemy.Init(_player);
    }

    private void SpawnRandom()
    {
        int enemyType = Random.Range(0, 2);
        Enemy = enemyType == 0 
            ? _gameFactory.CreateEnemy(EnemyTypeId.Tank, transform) 
            : _gameFactory.CreateEnemy(EnemyTypeId.Turret, transform);
        Enemy.Init(_player);
    }

    public void SetIsSlain()
    {
        _IsSlain = true;
    }
}
