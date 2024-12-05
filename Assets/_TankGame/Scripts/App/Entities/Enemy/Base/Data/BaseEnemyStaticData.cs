using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Data/Enemy Data/Static Data")]
public class BaseEnemyStaticData : ScriptableObject
{
    [SerializeField] private EnemyTypeId _enemyType;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _towerRotationSpeed;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _detectionDistance;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _detectionObstacleLayer;

    public EnemyTypeId EnemyType => _enemyType;
    public Enemy EnemyPrefab => _enemyPrefab;
    public float MaxHealth => _maxHealth;
    public float TowerRotationSpeed => _towerRotationSpeed;
    public float ReloadTime => _reloadTime;
    public float WallCheckDistance => _wallCheckDistance;
    public float DetectionDistance => _detectionDistance;
    public LayerMask PlayerLayer => _playerLayer;
    public LayerMask ObstacleLayer => _obstacleLayer;
    public LayerMask DetectionObstacleLayer => _detectionObstacleLayer;
}
