using TankGame.Core.Utils.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TankGame.App.Entities.Enemies.Base.Data
{
    [CreateAssetMenu(fileName = "BaseEnemyStaticData", menuName = "Static Data/Enemy Data/Base Enemy Static Data")]
    public class BaseEnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyTypeId _enemyType;
        [SerializeField] private AssetReferenceGameObject _prefabReference;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _towerRotationSpeed;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _wallCheckDistance;
        [SerializeField] private float _detectionDistance;
        [SerializeField] private ProjectileTypeId _projectileType;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private LayerMask _detectionObstacleLayer;

        public EnemyTypeId EnemyType => _enemyType;
        public AssetReferenceGameObject PrefabReference => _prefabReference;
        public float MaxHealth => _maxHealth;
        public float TowerRotationSpeed => _towerRotationSpeed;
        public float ReloadTime => _reloadTime;
        public float WallCheckDistance => _wallCheckDistance;
        public float DetectionDistance => _detectionDistance;
        public ProjectileTypeId ProjectileType => _projectileType;
        public LayerMask PlayerLayer => _playerLayer;
        public LayerMask ObstacleLayer => _obstacleLayer;
        public LayerMask DetectionObstacleLayer => _detectionObstacleLayer;
    }
}