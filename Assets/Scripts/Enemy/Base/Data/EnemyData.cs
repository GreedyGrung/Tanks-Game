using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _bodyRotationSpeed;
    [SerializeField] private float _towerRotationSpeed;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _detectionDistance;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    public float MaxHealth => _maxHealth;
    public float BodyRotationSpeed => _bodyRotationSpeed;
    public float TowerRotationSpeed => _towerRotationSpeed;
    public float ReloadTime => _reloadTime;
    public float WallCheckDistance => _wallCheckDistance;
    public float DetectionDistance => _detectionDistance;
    public LayerMask PlayerLayer => _playerLayer;
    public LayerMask ObstacleLayer => _obstacleLayer;
}
