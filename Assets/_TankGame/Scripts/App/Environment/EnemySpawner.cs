using Assets.Scripts.Data;
using Assets.Scripts.Services.PersistentProgress;
using TankGame.Scripts.App.Utils;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(UniqueId))]
public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    [SerializeField] private EnemyTypeId _enemyType;

    private string _id;
    private bool _IsSlain;
    private TurretFactory _turretFactory;
    private TankFactory _tankFactory;
    private Player _player;

    public Enemy Enemy { get; private set; }

    [Inject]
    private void Construct(TurretFactory turretFactory, TankFactory tankFactory)
    {
        _turretFactory = turretFactory;
        _tankFactory = tankFactory;
    }

    private void Awake()
    {
        _id = GetComponent<UniqueId>().Id;
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
        switch (_enemyType)
        {
            case EnemyTypeId.Tank:
                Enemy = _tankFactory.GetNewInstance(transform);
                break;
            case EnemyTypeId.Turret:
                Enemy = _turretFactory.GetNewInstance(transform);
                break;
            case EnemyTypeId.Random:
                int enemyType = Random.Range(0, 2);
                Enemy = enemyType == 0 ? _turretFactory.GetNewInstance(transform) : _tankFactory.GetNewInstance(transform);
                break;
            default:
                Enemy = _tankFactory.GetNewInstance(transform);
                break;
        }

        Enemy.Init(_player);
    }

    public void SetIsSlain()
    {
        _IsSlain = true;
    }
}
